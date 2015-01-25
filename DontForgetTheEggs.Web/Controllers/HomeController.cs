using System.Threading.Tasks;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Core.Helpers;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
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
            return View(new CreateGroceryListViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateGroceryListViewModel createGroceryListViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createGroceryListViewModel);
            }

            var request = new CreateGroceryList
            {
                Name = createGroceryListViewModel.Name
            };
            var newId = await _mediator.RequestAndEnsureAsync(request);
            return RedirectToAction("Index", "GroceryList", new { id = newId });
        }

        
    }
}