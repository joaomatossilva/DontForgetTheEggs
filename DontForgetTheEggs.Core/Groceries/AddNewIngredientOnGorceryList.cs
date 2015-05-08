using System.ComponentModel.DataAnnotations;
using ShortBus;

namespace DontForgetTheEggs.Core.Groceries
{
    public class AddNewIngredientOnGorceryList : IAsyncRequest<UnitType>
    {
        [Required]
        public int GroceryListId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Range(1, 10000)]
        public int Quantity { get; set; }
    }
}
