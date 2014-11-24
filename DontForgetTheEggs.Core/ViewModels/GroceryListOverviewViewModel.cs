using System.Collections.Generic;

namespace DontForgetTheEggs.Core.ViewModels
{
    public class GroceryListsOverviewViewModel
    {
        public IEnumerable<GroceryListOverview> GroceryLists { get; set; }
    }

    public class GroceryListOverview
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public int IngredientsCount { get; set; }
    }
}
