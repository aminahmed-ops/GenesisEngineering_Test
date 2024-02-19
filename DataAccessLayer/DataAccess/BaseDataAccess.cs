using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DataAccess
{
    public class BaseDataAccess:IBaseDataAccess
    {
        public string connectionString;
        private readonly IConfiguration _configuration;
        public BaseDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration["connectionStrings:BaseConnectionString"];
        }
    }
}
