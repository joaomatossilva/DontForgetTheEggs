using System.Linq;
using DontForgetTheEggs.Core.Queries;
using DontForgetTheEggs.Core.ViewModels;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Data.QueryHandlers
{
    public class GetGroceryListsOverviewHandler : IRequestHandler<GetGroceryListsOverview, GroceryListsOverviewViewModel>
    {
        private readonly EggsContext _eggsContext;

        public GetGroceryListsOverviewHandler(EggsContext eggsContext)
        {
            _eggsContext = eggsContext;
        }

        public GroceryListsOverviewViewModel Handle(GetGroceryListsOverview request)
        {
            var basequery = _eggsContext.GroceryLists.AsQueryable();
            if (!request.IncludeCompleted)
            {
                basequery = basequery.Where(groceryList => !groceryList.Completed);
            }

            var groceryLists = basequery.Select(groceryList => new GroceryListOverview
                                                               {
                                                                   Id = groceryList.Id,
                                                                   Completed = groceryList.Completed,
                                                                   Name = groceryList.Name,
                                                                   IngredientsCount = groceryList.Groceries.Count()
                                                               });
            return new GroceryListsOverviewViewModel {GroceryLists = groceryLists};
        }
    }
}
