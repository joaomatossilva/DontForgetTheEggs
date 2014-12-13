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

    }
}