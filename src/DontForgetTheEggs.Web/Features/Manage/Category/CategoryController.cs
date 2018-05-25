using System.Threading.Tasks;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Features.Manage.Category;
using MediatR;

namespace DontForgetTheEggs.Web.Features.Manage.Category
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Category
        public async Task<ActionResult> Index()
        {
            var model = await _mediator.Send(new Index.Query());
            return View(model);
        }

        // GET: Category/Detail/1
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

        // GET: Category/Edit/1
        public async Task<ActionResult> Edit(Edit.Query query)
        {
            var model = await _mediator.Send(query);
            return View(model);
        }

        // POST: Category/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Edit.Command command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Detail", new { command.Id });
        }

        // GET: Category/Delete/1
        public async Task<ActionResult> Delete(Delete.Query query)
        {
            var model = await _mediator.Send(query);
            return View(model);
        }

        // POST: Category/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Delete.Command command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Index");
        }
    }
}