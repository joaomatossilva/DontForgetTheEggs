using System.Threading.Tasks;
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

        public async Task<ActionResult> Detail(Detail.Query query)
        {
            var model = await _mediator.Send(query)
                .ConfigureAwait(false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Create.Command command)
        {
            var result = await _mediator.Send(command)
                .ConfigureAwait(false);
            return RedirectToAction("Detail", new { id = result});
        }

        public async Task<ActionResult> Edit(Edit.Query query)
        {
            var model = await _mediator.Send(query)
                .ConfigureAwait(false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Edit.Command command)
        {
            await _mediator.Send(command)
                .ConfigureAwait(false);
            return RedirectToAction("Detail", new { id = command.Id });
        }
    }
}