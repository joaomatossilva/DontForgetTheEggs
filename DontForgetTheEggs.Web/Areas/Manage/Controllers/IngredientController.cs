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

namespace DontForgetTheEggs.Web.Areas.Manage.Controllers
{
    public class IngredientController : Controller
    {
        private readonly IMediator _mediator;

        public IngredientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Manage/Ingredient
        public async Task<ActionResult> Index()
        {
            var ingredientsPerCategory = await _mediator.RequestAndEnsureAsync(new GetIngredientsPerCategory());
            return View(ingredientsPerCategory);
        }

        public async Task<ActionResult> Create()
        {
            await SetupCategoriesOptions();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateIngredient createIngredient)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.RequestAsync(createIngredient);
                if (!result.HasException())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("*", result.Exception.Message);
            }
            await SetupCategoriesOptions();
            return View(createIngredient);
        }

        private async Task SetupCategoriesOptions()
        {
            ViewBag.Categories = await _mediator.RequestAndEnsureAsync(new GetCategories());
        }

        public async Task<ActionResult> Edit(int id)
        {
            await SetupCategoriesOptions();
            var ingredient = await _mediator.RequestAndEnsureAsync(new GetIngredient {Id = id});
            return View(new EditIngredient
            {
                IngredientId = ingredient.Id,
                Name = ingredient.Name,
                CategoryId = ingredient.Category.Id
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditIngredient editIngredient)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.RequestAsync(editIngredient);
                if (!result.HasException())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("*", result.Exception.Message);
            }
            await SetupCategoriesOptions();
            return View(editIngredient);
        }
    }
}