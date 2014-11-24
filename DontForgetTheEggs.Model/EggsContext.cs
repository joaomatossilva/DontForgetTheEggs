using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Model
{
    public class EggsContext : DbContext
    {
        public DbSet<GroceryList> GroceryLists { get; set; }
        public DbSet<Grocery> Groceries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; } 
    }
}
