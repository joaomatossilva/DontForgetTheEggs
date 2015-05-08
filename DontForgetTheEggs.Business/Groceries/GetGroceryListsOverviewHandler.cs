using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Groceries;
using DontForgetTheEggs.Core.Groceries.ViewModels;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.Groceries
{
    public class GetGroceryListsOverviewHandler : IAsyncRequestHandler<GetGroceryListsOverview, GroceryListsOverviewViewModel>
    {
        private readonly EggsContext _eggsContext;

        public GetGroceryListsOverviewHandler(EggsContext eggsContext)
        {
            _eggsContext = eggsContext;
        }

        public async Task<GroceryListsOverviewViewModel> HandleAsync(GetGroceryListsOverview request)
        {
            var basequery = _eggsContext.GroceryLists.AsQueryable();
            if (!request.IncludeCompleted)
            {
                basequery = basequery.Where(groceryList => !groceryList.Completed);
            }

            var groceryLists = await basequery
                .Select(groceryList => new GroceryListOverview
                                       {
                                           Id = groceryList.Id,
                                           Completed = groceryList.Completed,
                                           Name = groceryList.Name,
                                           IngredientsCount = groceryList.Groceries.Count()
                                       })
                .ToListAsync();
            return new GroceryListsOverviewViewModel {GroceryLists = groceryLists};
        }
    }
}
