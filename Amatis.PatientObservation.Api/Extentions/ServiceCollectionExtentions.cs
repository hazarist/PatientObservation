using Amatis.PatientObservation.Business.Interfaces;
using Amatis.PatientObservation.Business.Services;
using Amatis.PatientObservation.Common.Models.PatientInfoModels;
using Amatis.PatientObservation.Common.Validators;
using Amatis.PatientObservation.DataAccess.Repository;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Amatis.PatientObservation.Api.Extentions
{
    public static class ServiceCollectionExtentions
    {
        public static void AddServiceInjections(this IServiceCollection services)
        {
            services.TryAddScoped<IPatientObservationService, PatientObservationService>();
            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
        }

        public static void AddModelValidators(this IServiceCollection services)
        {
            services.TryAddTransient<IValidator<PatientInfoPutModel>, PatientInfoPutModelValidator>();
            services.TryAddTransient<IValidator<PatientInfoPostModel>, PatientInfoPostModelValidator>();
        }
    }
}
