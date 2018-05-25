using System.Threading.Tasks;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Features.Manage.Grocery;
using MediatR;

namespace DontForgetTheEggs.Web.Features.Manage.Grocery
{
    public class GroceryController : Controller
    {
        private readonly IMediator _mediator;

        public GroceryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Grocery
        public async Task<ActionResult> Index()
        {
            var model = await _mediator.Send(new Index.Query());
            return View(model);
        }

        //GET: Grocery/Detail/1
        public async Task<ActionResult> Detail(Detail.Query query)
        {
            var model = await _mediator.Send(query);
            return View(model);
        }

        // GET: Grocery/Create/1
        public Task<ActionResult> Create()
        {
            var model = new Create.Command();
            return Task.FromResult((ActionResult)View(model));
        }

        // POST: Grocery/Create/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Create.Command command)
        {
            var result = await _mediator.Send(command);
            return RedirectToAction("Detail", new { id = result });
        }

        // GET: Grocery/Edit/1
        public async Task<ActionResult> Edit(Edit.Query query)
        {
            var model = await _mediator.Send(query);
            return View(model);
        }

        // POST: Grocery/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Edit.Command command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Detail", new { command.Id });
        }
    }
}