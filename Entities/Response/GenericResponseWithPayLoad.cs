using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response
{
    public class GenericResponseWithPayLoad<T> : GenericResponse
    {
        public T PayLoad { get; set; }
    }
}
