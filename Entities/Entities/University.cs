using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class University
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string WebSiteURL { get; set; }
        public string State { get; set; }
        public string InfoEmail { get; set; }
        public int CountryID { get; set; }
    }
}
