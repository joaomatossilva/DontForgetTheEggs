using DontForgetTheEggs.Core.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Core.Queries
{
    public class GetGroceryListsOverview : IRequest<GroceryListsOverviewViewModel>
    {
        public bool IncludeCompleted { get; set; }
    }
}
