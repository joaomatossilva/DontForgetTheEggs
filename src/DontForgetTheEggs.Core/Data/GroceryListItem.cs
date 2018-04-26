using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Core.Data
{
    public class GroceryListItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public bool Checked { get; set; }
        public DateTime? CheckedDatedUtc { get; set; }
        public int GroceryId { get; set; }
        public virtual Grocery Grocery { get; set; }
        public int GroceryListId { get; set; }
        public virtual GroceryList GroceryList { get; set; }
    }
}
