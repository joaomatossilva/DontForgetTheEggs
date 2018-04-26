using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Core.Data
{
    public class EggsDbContext : DbContext
    {
        public EggsDbContext(string connectionString)
            : base(connectionString)
        {
            //Disable EF Migrations, since we're using the FluentMigrator ones since they work better 
            //with feature branches.
            Database.SetInitializer<EggsDbContext>(null);
        }

        public DbSet<GroceryList> GroceryLists { get; set; }

        public DbSet<GroceryListItem> GroceryListItems { get; set; }

        public DbSet<Grocery> Groceries { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
