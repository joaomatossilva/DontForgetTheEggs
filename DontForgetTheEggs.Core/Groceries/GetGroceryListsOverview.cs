using DontForgetTheEggs.Core.Groceries.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Core.Groceries
{
    public class GetGroceryListsOverview : IAsyncRequest<GroceryListsOverviewViewModel>
    {
        public bool IncludeCompleted { get; set; }
    }

}
