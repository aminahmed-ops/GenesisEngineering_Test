using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Request
{
    public class GenericRequestWithPayLoad<T>
    {
        public T PayLoad { get; set; }
    }
}
