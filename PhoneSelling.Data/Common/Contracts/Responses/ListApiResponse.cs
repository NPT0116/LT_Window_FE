using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Contracts.Responses.Base
{
    public class ListApiResponse<T>
    {
        public List<T> Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
