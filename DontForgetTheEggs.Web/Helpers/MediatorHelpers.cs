using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShortBus;

namespace DontForgetTheEggs.Web.Helpers
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

        public static async Task<bool> RequestAndValidateAsync<TData>(this IMediator mediator, IAsyncRequest<TData> request, ModelStateDictionary modelState)
        {
            var response = await mediator.RequestAsync(request);
            if (response.HasException())
            {
                modelState.AddModelError("*", response.Exception.Message);
                return false;
            }
            return true;
        }
    }
}
