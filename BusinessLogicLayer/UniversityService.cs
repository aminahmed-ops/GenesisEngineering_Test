using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Entities.Entities;
using Entities.Request;
using Entities.Response;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayer
{
    public class UniversityService : IUniversityService
    {
        private readonly IUniversityDataAccess repository;
        private readonly ILogger<UniversityService> _logger;
        public UniversityService(IUniversityDataAccess universityDataAccess, ILogger<UniversityService> logger)
        {
         this.repository = universityDataAccess;
            _logger = logger;
        }

        public async Task<GenericResponse> SaveCountryAndUniversity(SaveCountryAndUniversityRequest request)
        {
            var result = new GenericResponse() { ApiStatusCode=System.Net.HttpStatusCode.OK};
            try
            {
                var country = await this.repository.SaveCountry(request.Country);

                if (country != null&&country.Id>0)
                {
                    request.Country = country;

                    var isUniversitySaved = await this.repository.SaveUniversities(request);

                    if (!isUniversitySaved)
                    {
                        result.ErrorMessage = "Some error occured please contact administrator";
                        result.ApiStatusCode = System.Net.HttpStatusCode.InternalServerError;
                        _logger.LogError("System is unable to update university repository.SaveUniversities response is false");
                    }

                }
                else
                {
                    result.ErrorMessage = "Some error occured please contact administrator";
                    result.ApiStatusCode = System.Net.HttpStatusCode.InternalServerError;
                    _logger.LogCritical("System is unable to save country repository.SaveCountry response is null");
                }

               
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Some error occured please contact administrator";
                result.ApiStatusCode = System.Net.HttpStatusCode.ExpectationFailed;
                _logger.LogCritical(ex.Message);
            }
            return result;
           
        }

        public async Task<GenericResponseWithPayLoad<IEnumerable<University>>> GetUniversityByCountryName(string countryName)
        {
            var result = new GenericResponseWithPayLoad<IEnumerable<University>>() { ApiStatusCode = System.Net.HttpStatusCode.OK };

            try
            {
                result.PayLoad=await this.repository.GetUniversityByCountryName(countryName);
            }
            catch (Exception ex)
            {

                result.ErrorMessage = "Some error occured please contact administrator";
                result.ApiStatusCode = System.Net.HttpStatusCode.ExpectationFailed;
                _logger.LogCritical(ex.Message);
            }
            return result;
        }

        public async Task<GenericResponse> UpdateUniversityByCountryNameAndID(University university, int universityID, string countryName)
        {
            var result = new GenericResponse() { ApiStatusCode=System.Net.HttpStatusCode.OK};
            try
            {
                var isSaved = await this.repository.UpdateUniversityByCountryNameAndID(university, universityID, countryName);

                if (!isSaved)
                {
                    result.ErrorMessage = "Some error occured please contact administrator";
                    result.ApiStatusCode = System.Net.HttpStatusCode.InternalServerError;
                    _logger.LogError("System is unable to update university repository.UpdateUniversityByCountryNameAndID response is false");
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "Some error occured please contact administrator";
                result.ApiStatusCode = System.Net.HttpStatusCode.ExpectationFailed;
                _logger.LogCritical(ex.Message);
            }
            return result;
        }
    }
}