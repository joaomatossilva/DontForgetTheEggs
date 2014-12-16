using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
using DontForgetTheEggs.Model;
using ShortBus;
using System.Data.Entity;

namespace DontForgetTheEggs.Data.QueryHandlers
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
