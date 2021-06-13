using Amatis.PatientObservation.Business.Interfaces;
using Amatis.PatientObservation.Common.Entities;
using Amatis.PatientObservation.Common.Exceptions;
using Amatis.PatientObservation.Common.Models.Filter;
using Amatis.PatientObservation.Common.Models.PatientInfoModels;
using Amatis.PatientObservation.DataAccess.Repository;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amatis.PatientObservation.Common.Models.ResponseWrapper;
using System.Linq.Expressions;
using System;
using LinqKit;
using System.Xml.Serialization;
using System.IO;
using WinSCP;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Amatis.PatientObservation.Common.Models.Settings;

namespace Amatis.PatientObservation.Business.Services
{
    public class PatientObservationService : IPatientObservationService
    {
        private readonly IRepository<PatientInfoEntity> repository;
        private readonly IMapper mapper;
        private readonly IHubContext<PatientMonitoringHub> hub;
        private readonly AppSettings settings;

        public PatientObservationService(IRepository<PatientInfoEntity> repository, IMapper mapper, IHubContext<PatientMonitoringHub> hub, IOptions<AppSettings> options)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.hub = hub;
            this.settings = options.Value;
        }

        public async Task<PatientInfoModel> GetByIdAsync(long id)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Patient info not found");
            }
            var model = mapper.Map<PatientInfoModel>(entity);
            return model;
        }

        public async Task<PagedResponse> GetAllAsync(PatientInfoQueryModel patientInfoQuery)
        {
            var validFilter = new PaginationFilter(patientInfoQuery.PageNumber, patientInfoQuery.PageSize);
            var predicate = PreparePredicateObject(patientInfoQuery);
            var pagedData = await repository.GetPagedListAsync(predicate, patientInfoQuery.PageNumber, patientInfoQuery.PageSize);
            var totalRecords = await repository.CountAsync();
            var mappedList = mapper.Map<List<PatientInfoModel>>(pagedData);
            return new PagedResponse(mappedList, validFilter.PageNumber, validFilter.PageSize, totalRecords);
        }

        public async Task<List<PatientInfoModel>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            return mapper.Map<List<PatientInfoModel>>(list);
        }

        private Expression<Func<PatientInfoEntity, bool>> PreparePredicateObject(PatientInfoQueryModel queryParams)
        {
            Expression<Func<PatientInfoEntity, bool>> predicate = PredicateBuilder.New<PatientInfoEntity>(true);

            if (queryParams != null)
            {
                if (!string.IsNullOrWhiteSpace(queryParams.Name))
                    predicate = predicate.And(entity => entity.Name.Contains(queryParams.Name));

                if (!string.IsNullOrWhiteSpace(queryParams.Surname))
                    predicate = predicate.And(entity => entity.Surname.Contains(queryParams.Surname));

                if (!string.IsNullOrWhiteSpace(queryParams.DoctorName))
                    predicate = predicate.And(entity => entity.DoctorName.Contains(queryParams.DoctorName));

                if (!string.IsNullOrWhiteSpace(queryParams.DoctorSurname))
                    predicate = predicate.And(entity => entity.DoctorSurname.Contains(queryParams.DoctorSurname));

                if (!string.IsNullOrWhiteSpace(queryParams.PoliclinicCode))
                    predicate = predicate.And(entity => entity.PoliclinicCode.Contains(queryParams.PoliclinicCode));

                if (!string.IsNullOrWhiteSpace(queryParams.DoctorRegistrationNumber))
                    predicate = predicate.And(entity => entity.DoctorRegistrationNumber.Contains(queryParams.DoctorRegistrationNumber));

                if (queryParams.Gender != null)
                    predicate = predicate.And(entity => entity.Gender == queryParams.Gender);

                if (queryParams.VisitStartDate.HasValue)
                    predicate = predicate.And(entity => entity.VisitDate >= queryParams.VisitStartDate);

                if (queryParams.VisitEndDate.HasValue)
                    predicate = predicate.And(entity => entity.VisitDate <= queryParams.VisitEndDate);

                if (queryParams.BirthStartDate.HasValue)
                    predicate = predicate.And(entity => entity.BirthDate >= queryParams.BirthStartDate);

                if (queryParams.BirthEndDate.HasValue)
                    predicate = predicate.And(entity => entity.BirthDate <= queryParams.BirthEndDate);
            }

            return predicate;
        }

        public async Task<PatientInfoModel> UpdateAsync(PatientInfoPutModel patientInfo)
        {
            var entity = await repository.GetByIdAsync(patientInfo.Id);
            if (entity == null)
            {
                throw new NotFoundException("Patient info not found");
            }
            var updatedEntity = mapper.Map(patientInfo, entity);
            await repository.UpdateAsync(updatedEntity);
            return mapper.Map<PatientInfoModel>(updatedEntity);
        }

        public async Task<PatientInfoModel> CreateAsync(PatientInfoPostModel patientInfo)
        {
            var entity = mapper.Map<PatientInfoEntity>(patientInfo);
            await repository.CreateAsync(entity);
            return mapper.Map<PatientInfoModel>(entity);
        }

        public async Task<PatientInfoModel> DeleteAsync(long id)
        {
            var entity = await repository.DeleteAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Patient info not found");
            }
            return mapper.Map<PatientInfoModel>(entity);
        }

        public async Task ReadXmlFileFromFtp()
        {
            SessionOptions sessionOptions = new SessionOptions
            {
                Protocol = Protocol.Ftp,
                HostName = settings.HostName,
                UserName = settings.UserName,
                Password = settings.Password
            };

            using (Session session = new Session())
            {
                session.Open(sessionOptions);
                string remotePath = settings.RemotePath;
                RemoteDirectoryInfo directory = session.ListDirectory(remotePath);

                foreach (RemoteFileInfo fileInfo in directory.Files)
                {
                    if (!fileInfo.IsDirectory &&
                        fileInfo.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        string tempPath = Path.GetRandomFileName();
                        var sourcePath =
                            RemotePath.EscapeFileMask(Path.Combine(remotePath, fileInfo.Name));
                        session.GetFiles(sourcePath, tempPath).Check();

                        using (var fileStream = File.Open(tempPath, FileMode.Open))
                        {
                            XmlSerializer xml = new XmlSerializer(typeof(PatientInfoPostModel));
                            try
                            {
                                var result = (PatientInfoPostModel)xml.Deserialize(fileStream);
                                await CreateAsync(result);
                                await hub.Clients.All.SendAsync("ReceiveData", result);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                        File.Delete(tempPath);
                    }
                }
            }
        }

    }
}
