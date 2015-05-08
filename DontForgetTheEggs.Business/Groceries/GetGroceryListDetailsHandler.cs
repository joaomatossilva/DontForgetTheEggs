using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Groceries;
using DontForgetTheEggs.Core.Groceries.ViewModels;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.Groceries
{
    public class GetGroceryListDetailsHandler : IAsyncRequestHandler<GetGroceryListDetails, GroceryListDetailsViewModel>
    {
        private readonly EggsContext _context;

        public GetGroceryListDetailsHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<GroceryListDetailsViewModel> HandleAsync(GetGroceryListDetails request)
        {
            var groceryList = await _context.GroceryLists
                .Include(x => x.Groceries)
                .Include(x => x.Groceries.Select(g => g.Ingredient))
                .Include(x => x.Groceries.Select(g => g.Ingredient.Category))
                .FirstAsync(x => x.Id == request.Id);

            //No need for async here because we already selected all the data from the grocery list
            var groceriesPerCategory = from g in groceryList.Groceries
                group g by g.Ingredient.Category
                into cats
                select new GroceriesCategoryViewModel
                       {
                           Count = cats.Count(),
                           Name = cats.Key.Name,
                           Groceries = cats.Select(gg => new GroceryViewModel
                                                         {
                                                             Checked = gg.Checked,
                                                             Name = gg.Ingredient.Name,
                                                             Quantity = gg.Quanity
                                                         })
                       };

            var model = new GroceryListDetailsViewModel
                        {
                            Id = groceryList.Id,
                            Completed = groceryList.Completed,
                            Name = groceryList.Name,
                            Categories = groceriesPerCategory
                        };
            return model;
        }
    }
}
