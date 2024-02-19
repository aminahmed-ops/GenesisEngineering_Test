using Entities.Entities;
using Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IUniversityDataAccess: IBaseDataAccess
    {
        Task<Country> SaveCountry(Country country);
        Task<bool> SaveUniversities(SaveCountryAndUniversityRequest saveCountryAndUniversityRequest);
        Task<IEnumerable<University>> GetUniversityByCountryName(string countryName);
        Task<bool> UpdateUniversityByCountryNameAndID(University university,int universityID , string countryName);
    }
}
