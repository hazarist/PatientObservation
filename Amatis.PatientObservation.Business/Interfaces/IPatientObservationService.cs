using Amatis.PatientObservation.Common.Models.PatientInfoModels;
using Amatis.PatientObservation.Common.Models.ResponseWrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amatis.PatientObservation.Business.Interfaces
{
    public interface IPatientObservationService
    {
        Task<PatientInfoModel> GetByIdAsync(long id);
        Task<List<PatientInfoModel>> GetAllAsync();
        Task<PagedResponse> GetAllAsync(PatientInfoQueryModel patientInfoQuery);
        Task<PatientInfoModel> UpdateAsync(PatientInfoPutModel patientInfo);
        Task<PatientInfoModel> DeleteAsync(long id);
        Task<PatientInfoModel> CreateAsync(PatientInfoPostModel patientInfo);
        Task ReadXmlFileFromFtp();
    }
}
