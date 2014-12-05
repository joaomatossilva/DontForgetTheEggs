using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortBus;

namespace DontForgetTheEggs.Core.Helpers
{
    public static class RequestResponseHelpers
    {
        public static void EnsureSuccessResponse<T>(this Response<T> response)
        {
            if (response.HasException())
            {
                throw response.Exception;
            }
        }
    }
}
