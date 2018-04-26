using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;

namespace DontForgetTheEggs.Migrations
{
    [Migration(20180426)]
    public class InitialSchema : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Categories")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey("PK__Categories")
                .WithColumn("Name").AsString(512).NotNullable();

            Create.Table("Groceries")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey("PK__Grocery")
                .WithColumn("Name").AsString(512).NotNullable()
                .WithColumn("CategoryId").AsInt32().NotNullable()
                    .ForeignKey("FK__Grocery_Category", "Categories", "Id");

            Create.Table("GroceryLists")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey("PK__GroceryLists")
                .WithColumn("Name").AsString(512).NotNullable()
                .WithColumn("DueIn").AsDateTime().NotNullable()
                .WithColumn("CreatedUtc").AsDateTime().NotNullable()
                .WithColumn("CompletedUtc").AsDateTime().Nullable()
                .WithColumn("Complete").AsBoolean().NotNullable();

            Create.Table("GroceryListItems")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey("PK__GroceryListItems")
                .WithColumn("Quantity").AsInt32().NotNullable()
                .WithColumn("Checked").AsBoolean().NotNullable()
                .WithColumn("CheckedDatedUtc").AsDateTime().Nullable()
                .WithColumn("GroceryId").AsInt32().NotNullable()
                    .ForeignKey("FK__GroceryListItems_Grocery", "Groceries", "Id")
                .WithColumn("GroceryListId").AsInt32().NotNullable()
                    .ForeignKey("FK__GroceryListItems_GroceryLists", "GroceryLists", "Id");
        }
    }
}
