using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Core.ViewModels
{
    public class AddIngredientViewModel
    {
        public IEnumerable<IngredientsCategoryViewModel> IngredientsList { get; set; }

        public int GroceryListId { get; set; }

        [Required]
        public int? IngredientId { get; set; }
        
        [Range(0,100000)]
        public int Quantity { get; set; }

        public string Ingredient { get; set; }
    }
}
