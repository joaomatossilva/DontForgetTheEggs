using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Categories;
using DontForgetTheEggs.Core.Categories.ViewModels;
using DontForgetTheEggs.Core.Ingedients;
using DontForgetTheEggs.Core.Ingedients.ViewModels;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.Ingedients
{
    public class GetIngredientHandler : IAsyncRequestHandler<GetIngredient, IngredientViewModel>
    {
        private readonly EggsContext _context;

        public GetIngredientHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<IngredientViewModel> HandleAsync(GetIngredient request)
        {
            return await _context.Ingredients
                .Include(i => i.Category)
                .Select(i => new IngredientViewModel
                {
                    Category = new CategoryViewModel
                    {
                        Id = i.Category.Id,
                        Name = i.Category.Name
                    },
                    Id = i.Id,
                    Name = i.Name
                })
                .SingleOrDefaultAsync(i => i.Id == request.Id);
        }
    }
}
