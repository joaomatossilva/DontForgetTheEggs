using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Core.ViewModels
{
    public class IngredientsCategoryViewModel : CategoryViewModel
    {
        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
    }
}
