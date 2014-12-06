using DontForgetTheEggs.Core.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Core.Queries
{
    public class GetGroceryListDetails : IRequest<GroceryListDetailsViewModel>
    {
        public int Id { get; set; }
    }
}
