using Amatis.PatientObservation.Common.Entities;
using Amatis.PatientObservation.Common.Models.PatientInfoModels;
using AutoMapper;

namespace Amatis.PatientObservation.Business.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PatientInfoEntity, PatientInfoModel>().ReverseMap();
            CreateMap<PatientInfoEntity, PatientInfoPostModel>().ReverseMap();
            CreateMap<PatientInfoEntity, PatientInfoPutModel>().ReverseMap();
        }
    }
}
