using System.Threading.Tasks;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Features.Home;
using MediatR;

namespace DontForgetTheEggs.Web.Features.Home
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _mediator.Send(new Index.Query());
            return View(model);
        }
    }
}