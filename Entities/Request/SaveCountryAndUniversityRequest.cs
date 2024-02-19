using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Request
{
    public class SaveCountryAndUniversityRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public Country Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<University> Universities{ get; set; }
    }
}
