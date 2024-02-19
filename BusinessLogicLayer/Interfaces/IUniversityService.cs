using Entities.Entities;
using Entities.Request;
using Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUniversityService
    {
        Task<GenericResponse> SaveCountryAndUniversity(SaveCountryAndUniversityRequest request);
        Task<GenericResponseWithPayLoad<IEnumerable<University>>> GetUniversityByCountryName(string countryName);
        Task<GenericResponse> UpdateUniversityByCountryNameAndID(University university, int universityID, string countryName);
    }
}
