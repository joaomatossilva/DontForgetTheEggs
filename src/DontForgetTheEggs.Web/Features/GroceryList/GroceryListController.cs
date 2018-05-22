using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Features.GroceryList;
using MediatR;

namespace DontForgetTheEggs.Web.Features.GroceryList
{
    public class GroceryListController : Controller
    {
        private readonly IMediator _mediator;

        public GroceryListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<ActionResult> Create()
        {
            var model = new Create.Command();
            return Task.FromResult((ActionResult)View(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Create.Command command)
        {
            var result = await _mediator.Send(command)
                .ConfigureAwait(false);
            return RedirectToAction("Details", new { id = result});
        }
    }
}