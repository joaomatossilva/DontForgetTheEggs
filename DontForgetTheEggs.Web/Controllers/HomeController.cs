using System.Threading.Tasks;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
using DontForgetTheEggs.Web.Helpers;
using ShortBus;

namespace DontForgetTheEggs.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<ActionResult> Index(bool? includeCompleted)
        {
            var request = new GetGroceryListsOverview { IncludeCompleted = includeCompleted == true};
            var groceriesOverView = await _mediator.RequestAndEnsureAsync(request);
            return View(groceriesOverView);
        }

        public ActionResult Create()
        {
            return View(new CreateGroceryList());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateGroceryList model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var newId = await _mediator.RequestAndEnsureAsync(model);
            return RedirectToAction("Index", "GroceryList", new { id = newId });
        }

        
    }
}