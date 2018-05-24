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

        // GET: Category
        public async Task<ActionResult> Index()
        {
            var model = await _mediator.Send(new Index.Query());
            return View(model);
        }
    }
}