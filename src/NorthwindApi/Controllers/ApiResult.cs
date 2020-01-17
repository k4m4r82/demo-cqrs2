using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwindApi.Controllers
{
    public class ApiResult
    {
        public ApiResultStatus status { get; set; }
        public object results { get; set; }
    }

    public class ApiResultStatus
    {
        public ApiResultStatus()
        {
            errors = new List<string>();
        }

        public int code { get; set; }        
        public string description { get; set; }
        public int pages_count { get; set; }
        public List<string> errors { get; set; }
    }
}
