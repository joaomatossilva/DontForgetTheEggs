using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Core.ViewModels
{
    public class IngredientsCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<IngredientViewModel> Ingredients { get; set; }
    }
}
