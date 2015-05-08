using DontForgetTheEggs.Core.Groceries.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Core.Groceries
{
    public class GetGroceryListDetails : IAsyncRequest<GroceryListDetailsViewModel>
    {
        public int Id { get; set; }
    }
}
