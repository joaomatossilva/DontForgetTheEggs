using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
using DontForgetTheEggs.Web.Helpers;
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
            await SetupCategoriesOptions();
            var model = new AddNewIngredientOnGorceryList
            {
                GroceryListId = id,
                Quantity = 1
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddNewIngredient(AddNewIngredientOnGorceryList model)
        {
            if (ModelState.IsValid)
            {
                if (await _mediator.RequestAndValidateAsync(model, ModelState))
                {
                    return RedirectToAction("Index", new { id = model.GroceryListId });
                }
            }
            await SetupCategoriesOptions();
            return View(model);
        }

        private async Task SetupCategoriesOptions()
        {
            ViewBag.Categories = await _mediator.RequestAndEnsureAsync(new GetCategories());
        }

        public async Task<ActionResult> AddIngredient(int id)
        {
            var model = new AddIngredientOnGorceryList
                            {
                                GroceryListId = id,
                                Quantity = 1
                            };
            await SetupIngredientsPerCategoryOptions();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddIngredient(AddIngredientOnGorceryList model)
        {
            if (ModelState.IsValid)
            {
                if (await _mediator.RequestAndValidateAsync(model, ModelState))
                {
                    return RedirectToAction("Index", new { id = model.GroceryListId });
                }
            }
            await SetupIngredientsPerCategoryOptions();
            return View(model);
        }

        private async Task SetupIngredientsPerCategoryOptions()
        {
            ViewBag.IngredientsList = await _mediator.RequestAndEnsureAsync(new GetIngredientsPerCategory());
        }
    }
}