using System.ComponentModel.DataAnnotations;

namespace DontForgetTheEggs.Core.Categories.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
