using BusinessLogicLayer.Interfaces;
using Entities.Entities;
using Entities.Request;
using Entities.Response;
using Microsoft.AspNetCore.Mvc;

namespace GenesisEngineering_Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityService _universityService;
       
        private readonly ILogger<UniversityController> _logger;

        public UniversityController(ILogger<UniversityController> logger,IUniversityService universityService)
        {
            _logger = logger;
            _universityService = universityService;
        }

        [HttpPost("SaveCountryAndUniversity")]
        //[Route("SaveCountryAndUniversity")]
        public async Task<GenericResponse> SaveCountryAndUniversity([FromBody] GenericRequestWithPayLoad<SaveCountryAndUniversityRequest> request)
        {

            var result =await this._universityService.SaveCountryAndUniversity(request.PayLoad);

            return result;
        }
        [HttpPost("UpdateUniversityByCountryNameAndID/{universityID}/CountryName/{countryName}")]
        //[Route("UpdateUniversityByCountryNameAndID/{universityID}/CountryName/{countryName}")]
        public async Task<GenericResponse> UpdateUniversityByCountryNameAndID([FromBody] GenericRequestWithPayLoad<University> request, int universityID, string countryName)
        {

            var result = await this._universityService.UpdateUniversityByCountryNameAndID(request.PayLoad, universityID, countryName);

            return result;
        }

        [HttpGet("GetUniversityByCountryName/{countryName}")]
        //[Route("GetUniversityByCountryName/{countryName}")]
        public async Task<GenericResponseWithPayLoad<IEnumerable<University>>> GetUniversityByCountryName(string countryName)
        {

            var result =await this._universityService.GetUniversityByCountryName(countryName);

            return result;
        }
    }
}