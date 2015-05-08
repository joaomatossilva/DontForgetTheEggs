using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Web.Helpers;
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
        public async Task<ActionResult> Index()
        {
            var categories = await _mediator.RequestAndEnsureAsync(new GetCategories());
            return View(categories);
        }

        // GET: Manage/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/Category/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateCategory newCategory)
        {
            if (ModelState.IsValid)
            {
                if (await _mediator.RequestAndValidateAsync(newCategory, ModelState))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(newCategory);
        }

        // GET: Manage/Category/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //TODO: I shortcutted here
            var category = (await _mediator.RequestAndEnsureAsync(new GetCategories()))
                .Single(c => c.Id == id);
            return View(category);
        }

        // POST: Manage/Category/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(RenameCategory category)
        {
            if (ModelState.IsValid)
            {
                if(await _mediator.RequestAndValidateAsync(category, ModelState));
                {
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

    }
}
