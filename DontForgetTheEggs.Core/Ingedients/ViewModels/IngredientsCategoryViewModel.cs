using System.Collections.Generic;
using DontForgetTheEggs.Core.Categories.ViewModels;

namespace DontForgetTheEggs.Core.Ingedients.ViewModels
{
    public class IngredientsCategoryViewModel : CategoryViewModel
    {
        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
    }
}
