using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class AddIngredientOnGorceryList : IAsyncRequest<UnitType>
    {
        public int GroceryListId { get; set; }
        public int Quantity { get; set; }
        public int IngredientId { get; set; }
    }
}
