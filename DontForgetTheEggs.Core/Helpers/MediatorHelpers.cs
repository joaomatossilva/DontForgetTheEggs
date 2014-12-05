using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortBus;

namespace DontForgetTheEggs.Core.Helpers
{
    public static class MediatorHelpers
    {
        public static TData RequestAndEnsure<TData>(this IMediator mediator, IRequest<TData> request)
        {
            var response = mediator.Request(request);
            response.EnsureSuccessResponse();
            return response.Data;
        }
    }
}
