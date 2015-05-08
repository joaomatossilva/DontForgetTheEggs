using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Categories.ViewModels;
using DontForgetTheEggs.Core.Ingedients;
using DontForgetTheEggs.Core.Ingedients.ViewModels;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.Ingedients
{
    public class GetIngredientsPerCategoryHandler : IAsyncRequestHandler<GetIngredientsPerCategory, IEnumerable<IngredientsCategoryViewModel>>
    {
        private readonly EggsContext _context;

        public GetIngredientsPerCategoryHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IngredientsCategoryViewModel>> HandleAsync(GetIngredientsPerCategory request)
        {
            return await (from c in _context.Categories
                select new IngredientsCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Ingredients = c.Ingredients.Select(i => new IngredientViewModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Category = new CategoryViewModel
                        {
                            Id = c.Id,
                            Name = c.Name
                        }
                    })
                }).ToListAsync();
        }
    }
}
