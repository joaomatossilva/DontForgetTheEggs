using DontForgetTheEggs.Core.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Core.Queries
{
    public class GetGroceryListsOverview : IAsyncRequest<GroceryListsOverviewViewModel>
    {
        public bool IncludeCompleted { get; set; }
    }
}
