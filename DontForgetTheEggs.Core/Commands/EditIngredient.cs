using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class EditIngredient : IAsyncRequest<UnitType>
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string NewCategoryName { get; set; }
    }
}
