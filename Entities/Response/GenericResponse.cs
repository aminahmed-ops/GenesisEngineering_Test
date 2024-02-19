using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response
{
    public class GenericResponse
    {
        public HttpStatusCode ApiStatusCode{ get; set; }
        public string ErrorMessage { get; set; }
    }
}
