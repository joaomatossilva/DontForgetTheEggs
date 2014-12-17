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
        /// <summary>
        /// Executes the request and ensures that it executed successfully.
        /// If any error occurred on during the request execution it will be thrown
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="mediator"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<TData> RequestAndEnsureAsync<TData>(this IMediator mediator, IAsyncRequest<TData> request)
        {
            var response = await mediator.RequestAsync(request);
            if (response.HasException())
            {
                throw new Exception("Error occurred during request execution", response.Exception);
            }
            return response.Data;
        }
    }
}
