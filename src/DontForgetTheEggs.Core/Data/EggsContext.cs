using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Must reference a type in EntityFramework.SqlServer.dll so that this dll will be
// included in the output folder of referencing projects without requiring a direct 
// dependency on Entity Framework. See http://stackoverflow.com/a/22315164/1141360.
using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;

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
