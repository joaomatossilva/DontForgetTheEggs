using System.Collections.Generic;
using System.Linq;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Data.QueryHandlers
{
    public class GetCategoriesHandler : IRequestHandler<GetCategories, IEnumerable<CategoryViewModel>>
    {
        private readonly EggsContext _context;

        public GetCategoriesHandler(EggsContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryViewModel> Handle(GetCategories request)
        {
            return _context.Categories.Select(c => new CategoryViewModel {Id = c.Id, Name = c.Name});
        }
    }
}
