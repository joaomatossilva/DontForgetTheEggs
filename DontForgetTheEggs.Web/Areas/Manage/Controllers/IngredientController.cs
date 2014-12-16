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

        public ActionResult Create()
        {
            var categories = _mediator.RequestAndEnsure(new GetCategories());
            return View(new NewIngredientViewModel{ Categories = categories});
        }

        [HttpPost]
        public async Task<ActionResult> Create(NewIngredientViewModel newIngredientViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.RequestAsync(new CreateIngredient
                {
                    Name = newIngredientViewModel.Name,
                    CategoryId = newIngredientViewModel.CategoryId,
                    NewCategoryName = newIngredientViewModel.NewCategoryName
                });
                if (!result.HasException())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("*", result.Exception.Message);
            }
            newIngredientViewModel.Categories = _mediator.RequestAndEnsure(new GetCategories());
            return View(newIngredientViewModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var categories = _mediator.RequestAndEnsure(new GetCategories());
            var ingredient = await _mediator.RequestAndEnsureAsync(new GetIngredient {Id = id});
            return View(new EditIngredientViewModel
            {
                Categories = categories, 
                IngredientId = ingredient.Id,
                Name = ingredient.Name,
                CategoryId = ingredient.Category.Id
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditIngredientViewModel editIngredientViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.RequestAsync(new EditIngredient
                {
                    IngredientId = editIngredientViewModel.IngredientId
                    Name = editIngredientViewModel.Name,
                    CategoryId = editIngredientViewModel.CategoryId,
                    NewCategoryName = editIngredientViewModel.NewCategoryName
                });
                if (!result.HasException())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("*", result.Exception.Message);
            }
            editIngredientViewModel.Categories = _mediator.RequestAndEnsure(new GetCategories());
            return View(editIngredientViewModel);
        }
    }
}