using PhoneSelling.Data.Contracts.Responses.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Common.Utils
{
    public static class ApiResponseUtil
    {

        public static ApiResponse<T> EnsureSuccess<T>(ApiResponse<T> response)
        {
            if (response == null)
            {
                throw new Exception("No response from API.");
            }

            if (!response.Succeeded)
            {
                string errorMessage = !string.IsNullOrWhiteSpace(response.Message)
                                        ? response.Message
                                        : (response.Errors != null && response.Errors.Any()
                                            ? string.Join(", ", response.Errors)
                                            : "API call was not successful.");
                throw new Exception(errorMessage);
            }

            return response;
        }
    }
}
