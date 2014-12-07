using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Core.Helpers;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Web.Areas.Manage.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Manage/Category
        public ActionResult Index()
        {
            var categories = _mediator.RequestAndEnsure(new GetCategories());
            return View(categories);
        }

        // GET: Manage/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(NewCategoryViewModel newCategoryView)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.RequestAsync(new CreateCategory {Name = newCategoryView.Name});
                if (!result.HasException())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("*", result.Exception.Message);
            }
            return View(newCategoryView);
        }

        // GET: Manage/Category/Edit/5
        public ActionResult Edit(int id)
        {
            //TODO: I shortcutted here
            var category = _mediator.RequestAndEnsure(new GetCategories())
                .Single(c => c.Id == id);
            return View(category);
        }

        // POST: Manage/Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.RequestAsync(new RenameCategory { Id = model.Id, Name = model.Name });
                if (!result.HasException())
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result.Exception.Message);
            }
            return View(model);
        }

    }
}
