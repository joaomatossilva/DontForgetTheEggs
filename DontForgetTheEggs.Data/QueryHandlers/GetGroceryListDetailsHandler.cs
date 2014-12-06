using System.Data.Entity;
using System.Linq;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Data.QueryHandlers
{
    public class GetGroceryListDetailsHandler : IRequestHandler<GetGroceryListDetails, GroceryListDetailsViewModel>
    {
        private readonly EggsContext _context;

        public GetGroceryListDetailsHandler(EggsContext context)
        {
            _context = context;
        }

        public GroceryListDetailsViewModel Handle(GetGroceryListDetails request)
        {
            var groceryList = _context.GroceryLists
                .Include(x => x.Groceries)
                .Include(x => x.Groceries.Select(g => g.Ingredient))
                .Include(x => x.Groceries.Select(g => g.Ingredient.Category))
                .First(x => x.Id == request.Id);

            var groceries = from g in groceryList.Groceries
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
                            Categories = groceries
                        };
            return model;
        }
    }
}
