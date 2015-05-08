using System.ComponentModel.DataAnnotations;
using ShortBus;

namespace DontForgetTheEggs.Core.Groceries
{
    public class AddIngredientOnGorceryList : IAsyncRequest<UnitType>
    {
        public int GroceryListId { get; set; }
        [Range(0, 100000)]
        public int Quantity { get; set; }
        [Required]
        public int IngredientId { get; set; }
    }
}
