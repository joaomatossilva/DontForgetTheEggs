using System.Web.Mvc;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Core.Helpers;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Web.ViewModels;
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


        public ActionResult Index(bool? includeCompleted)
        {
            var request = new GetGroceryListsOverview { IncludeCompleted = includeCompleted == true};
            var groceriesOverView = _mediator.RequestAndEnsure(request);
            return View(groceriesOverView);
        }

        public ActionResult Create()
        {
            return View(new CreateGroceryListViewModel());
        }

        [HttpPost]
        public ActionResult Create(CreateGroceryListViewModel createGroceryListViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createGroceryListViewModel);
            }

            var request = new CreateGroceryList
            {
                Name = createGroceryListViewModel.Name
            };
            var newId = _mediator.RequestAndEnsure(request);
            return RedirectToAction("Details", new { id = newId});
        }

        public ActionResult Details(int id)
        {
            return View();
        }

    }
}