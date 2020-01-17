using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NorthwindApi.Controllers
{
    public static class ApiResultHelper
    {
        public static object GenerateResponse(HttpStatusCode httpStatusCode)
        {
            var output = new
            {
                status = new
                {
                    code = (int)httpStatusCode,
                    description = httpStatusCode.ToString()
                }
            };

            return output;
        }

        public static object GenerateResponse(HttpStatusCode httpStatusCode, object result)
        {
            var output = new
            {
                status = new
                {
                    code = (int)httpStatusCode,
                    description = httpStatusCode.ToString()
                },

                result
            };

            return output;
        }

        public static object GenerateResponse(HttpStatusCode httpStatusCode, object results, int pagesCount)
        {
            var output = new
            {
                status = new
                {
                    code = (int)httpStatusCode,
                    description = httpStatusCode.ToString(),
                    pages_count = pagesCount,
                },

                results
            };

            return output;
        }

        public static object GenerateErrorResponse(HttpStatusCode httpStatusCode, object errors)
        {
            var output = new
            {
                status = new
                {
                    code = (int)httpStatusCode,
                    description = httpStatusCode.ToString()
                },

                errors
            };

            return output;
        }
    }
}
