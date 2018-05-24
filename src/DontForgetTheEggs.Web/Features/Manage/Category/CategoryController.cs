﻿using System.Threading.Tasks;
using System.Web.Mvc;
using DontForgetTheEggs.Core.Features.Manage.Category;
using MediatR;

namespace DontForgetTheEggs.Web.Features.Manage.Category
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Category
        public async Task<ActionResult> Index()
        {
            var model = await _mediator.Send(new Index.Query());
            return View(model);
        }
    }
}