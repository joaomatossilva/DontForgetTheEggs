using ShortBus;

namespace DontForgetTheEggs.Core.Groceries
{
    public class SetGroceryListCompleted : IAsyncRequest<UnitType>
    {
        public int GroceryListId { get; set; }
        public bool Completed { get; set; }
    }
}
