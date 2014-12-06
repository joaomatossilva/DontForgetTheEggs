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
            return RedirectToAction("GroceryList", new { id = newId });
        }

        public ActionResult GroceryList(int id)
        {
            var request = new GetGroceryListDetails {Id = id};
            var viewModel = _mediator.RequestAndEnsure(request);
            return View(viewModel);
        }

        public ActionResult AddNewIngredient(int id)
        {
            var categories = _mediator.RequestAndEnsure(new GetCategories());
            var model = new AddNewIngredientViewModel
                        {
                            GroceryListId = id,
                            Categories = categories,
                            Quantity = 1
                        };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddNewIngredient(AddNewIngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = new AddNewIngredientOnGorceryList
                              {
                                  GroceryListId = model.GroceryListId,
                                  IngredientName = model.Name,
                                  Quantity = model.Quantity,
                                  CategoryId = model.CategoryId,
                                  CategoryName = model.NewCategoryName
                              };
                var result = _mediator.Request(request);
                if (!result.HasException())
                {
                    return RedirectToAction("GroceryList", new {id = model.GroceryListId});
                }

                ModelState.AddModelError("", result.Exception);
            }
            model.Categories = _mediator.RequestAndEnsure(new GetCategories());
            return View(model);
        }
    }
}