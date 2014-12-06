using System.Collections.Generic;

namespace DontForgetTheEggs.Core.ViewModels
{
    public class GroceryListDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
        public IEnumerable<GroceriesCategoryViewModel> Categories { get; set; } 
    }

    public class GroceriesCategoryViewModel
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public IEnumerable<GroceryViewModel> Groceries { get; set; }
    }

    public class GroceryViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool Checked { get; set; }
    }
}
