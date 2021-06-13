using Amatis.PatientObservation.Business.Interfaces;
using Amatis.PatientObservation.Common.Models.PatientInfoModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Amatis.PatientObservation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientObservationController : Controller
    {
        private readonly IPatientObservationService patientInfoService;
        public PatientObservationController(IPatientObservationService patientInfoService)
        {
            this.patientInfoService = patientInfoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var serviceRepsonse = await patientInfoService.GetByIdAsync(id);
            return Ok(serviceRepsonse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PatientInfoQueryModel patientInfoQuery)
        {
            var serviceRepsonse = await patientInfoService.GetAllAsync(patientInfoQuery);
            return Ok(serviceRepsonse);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientInfoPostModel patientInfo)
        {
            var serviceRepsonse = await patientInfoService.CreateAsync(patientInfo);
            return Ok(serviceRepsonse);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PatientInfoPutModel patientInfo)
        {
            var serviceRepsonse = await patientInfoService.UpdateAsync(patientInfo);
            return Ok(serviceRepsonse);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var serviceRepsonse = await patientInfoService.DeleteAsync(id);
            return Ok(serviceRepsonse);
        }

    }
}
