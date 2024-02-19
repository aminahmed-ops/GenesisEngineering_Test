using DataAccessLayer.Interfaces;
using Entities.Entities;
using Entities.Request;


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CommonUtilities;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;

namespace DAL.DataAccess
{
    public class UniversityDataAccess : IUniversityDataAccess
    {
        public string connectionString;
        private readonly IConfiguration _configuration;

        #region SP names

        private readonly string SPSaveCountry = "[dbo].[SaveCountry]";
        private readonly string SPSaveUniversity = "[dbo].[SaveUniversity]";
        private readonly string SPGetUniversityByCountryName = "[dbo].[GetUniversityByCountryName]";
        private readonly string SPUpdateUniversityByCountryNameAndID = "[dbo].[UpdateUniversityByCountryNameAndID]";


        #endregion
        public UniversityDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["connectionStrings:BaseConnectionString"];
        }
        public async Task<Country> SaveCountry(Country country)
        {
            DataSet ds = new DataSet();
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlComm = new SqlCommand(SPSaveCountry, conn);
                sqlComm.Parameters.AddWithValue("@Name", country.Name);
                sqlComm.Parameters.AddWithValue("@Code", country.Code);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count>0)
            {
                country = ds.Tables[0].ToList<Country>().FirstOrDefault();
            }


            return country;
        }

        public async Task<bool> SaveUniversities(SaveCountryAndUniversityRequest saveCountryAndUniversityRequest)
        {
            IsSaved isDataSaved = new IsSaved { IsSavedSuccessfuly=false};

            DataSet ds = new DataSet();
            DataTable universityDataTable = CommonUtilities.Extensions.GetDataTableFromUniversities(saveCountryAndUniversityRequest.Universities);
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlComm = new SqlCommand(SPSaveUniversity, conn);
                sqlComm.Parameters.AddWithValue("@CountryID", saveCountryAndUniversityRequest.Country.Id);
                sqlComm.Parameters.AddWithValue("@University_Type", universityDataTable);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                isDataSaved = ds.Tables[0].ToList<IsSaved>().FirstOrDefault();
            }


            return isDataSaved.IsSavedSuccessfuly;
        }

        public async Task<IEnumerable<University>> GetUniversityByCountryName(string countryName)
        {
            var universities = new List<University>();
            DataSet ds = new DataSet();
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlComm = new SqlCommand(SPGetUniversityByCountryName, conn);
                sqlComm.Parameters.AddWithValue("@countryName", countryName);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                universities = ds.Tables[0].ToList<University>();
            }


            return universities;
        }

        public async Task<bool> UpdateUniversityByCountryNameAndID(University university, int universityID, string countryName)
        {
            IsSaved isDataSaved = new IsSaved() {IsSavedSuccessfuly=false };
            DataSet ds = new DataSet();
            using (var conn = new SqlConnection(connectionString))
            {
                SqlCommand sqlComm = new SqlCommand(SPUpdateUniversityByCountryNameAndID, conn);
                sqlComm.Parameters.AddWithValue("@universityID", universityID);
                sqlComm.Parameters.AddWithValue("@countryName", countryName);
                sqlComm.Parameters.AddWithValue("@Name", university.Name);
                sqlComm.Parameters.AddWithValue("@State", university.State);
                sqlComm.Parameters.AddWithValue("@InfoEmail", university.InfoEmail);
                sqlComm.Parameters.AddWithValue("@WebSiteURL", university.WebSiteURL);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                isDataSaved = ds.Tables[0].ToList<IsSaved>().FirstOrDefault();
            }


            return isDataSaved.IsSavedSuccessfuly;
        }
    }
}
