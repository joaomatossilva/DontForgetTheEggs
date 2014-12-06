using System.ComponentModel.DataAnnotations;

namespace DontForgetTheEggs.Core.ViewModels
{
    public class CreateGroceryListViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}