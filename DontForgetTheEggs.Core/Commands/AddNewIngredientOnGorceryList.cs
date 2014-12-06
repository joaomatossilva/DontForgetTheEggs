using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class AddNewIngredientOnGorceryList : IRequest<UnitType>
    {
        public int GroceryListId { get; set; }
        public int Quantity { get; set; }
        public string IngredientName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
