using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.QueryHandlers
{
    public class GetCategoriesHandler : IAsyncRequestHandler<GetCategories, IEnumerable<CategoryViewModel>>
    {
        private readonly EggsContext _context;

        public GetCategoriesHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryViewModel>> HandleAsync(GetCategories request)
        {
            return await _context.Categories.Select(c => new CategoryViewModel {Id = c.Id, Name = c.Name})
                .ToListAsync();
        }
    }
}
