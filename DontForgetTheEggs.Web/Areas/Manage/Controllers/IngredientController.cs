using System.Threading.Tasks;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Categories;
using DontForgetTheEggs.Core.Ingedients;
using DontForgetTheEggs.Web.Helpers;
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
                if (await _mediator.RequestAndValidateAsync(createIngredient, ModelState))
                {
                    return RedirectToAction("Index");
                }
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
                if (await _mediator.RequestAndValidateAsync(editIngredient, ModelState))
                {
                    return RedirectToAction("Index");
                }
            }
            await SetupCategoriesOptions();
            return View(editIngredient);
        }
    }
}