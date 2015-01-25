using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Core.Helpers;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Web.Controllers
{
    public class GroceryListController : Controller
    {
        private readonly IMediator _mediator;

        public GroceryListController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index(int id)
        {
            var request = new GetGroceryListDetails { Id = id };
            var viewModel = await _mediator.RequestAndEnsureAsync(request);
            return View(viewModel);
        }

        public async Task<ActionResult> AddNewIngredient(int id)
        {
            var categories = await _mediator.RequestAndEnsureAsync(new GetCategories());
            var model = new AddNewIngredientViewModel
            {
                GroceryListId = id,
                Categories = categories,
                Quantity = 1
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewIngredient(AddNewIngredientViewModel model)
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
                var result = await _mediator.RequestAsync(request);
                if (!result.HasException())
                {
                    return RedirectToAction("Index", new { id = model.GroceryListId });
                }

                ModelState.AddModelError("", result.Exception);
            }
            model.Categories = await _mediator.RequestAndEnsureAsync(new GetCategories());
            return View(model);
        }

        public async Task<ActionResult> AddIngredient(int id)
        {
            var ingredientsPerCategry = await _mediator.RequestAndEnsureAsync(new GetIngredientsPerCategory());
            var viewModel = new AddIngredientViewModel
                            {
                                IngredientsList = ingredientsPerCategry,
                                GroceryListId = id,
                                Quantity = 1
                            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> AddIngredient(AddIngredientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var request = new AddIngredientOnGorceryList
                              {
                                  GroceryListId = model.GroceryListId,
                                  IngredientId = model.IngredientId.Value,
                                  Quantity = model.Quantity
                              };
                var result = await _mediator.RequestAsync(request);
                if (!result.HasException())
                {
                    return RedirectToAction("Index", new { id = model.GroceryListId });
                }

                ModelState.AddModelError("", result.Exception);
            }
            model.IngredientsList = await _mediator.RequestAndEnsureAsync(new GetIngredientsPerCategory());
            return View(model);
        }
    }
}