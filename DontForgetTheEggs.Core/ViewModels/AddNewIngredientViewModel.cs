using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Core.ViewModels
{
    public class AddNewIngredientViewModel
    {
        [Required]
        public int GroceryListId { get; set; }
        [Required]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public string NewCategoryName { get; set; }
        [Range(1,10000)]
        public int Quantity { get; set; }
    }
}
