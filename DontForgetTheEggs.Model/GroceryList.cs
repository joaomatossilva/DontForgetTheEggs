using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Model
{
    public class GroceryList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public virtual ICollection<Grocery> Groceries { get; set; }
    }
}
