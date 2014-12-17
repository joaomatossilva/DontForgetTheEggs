using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class SetGroceryListCompleted : IAsyncRequest<UnitType>
    {
        public int GroceryListId { get; set; }
        public bool Completed { get; set; }
    }
}
