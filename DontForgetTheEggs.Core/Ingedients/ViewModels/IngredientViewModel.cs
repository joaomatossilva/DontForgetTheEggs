using DontForgetTheEggs.Core.Categories.ViewModels;

namespace DontForgetTheEggs.Core.Ingedients.ViewModels
{
    public class IngredientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryViewModel Category { get; set; }
    }
}