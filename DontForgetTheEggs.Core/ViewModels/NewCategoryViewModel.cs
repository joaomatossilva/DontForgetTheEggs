using System.ComponentModel.DataAnnotations;

namespace DontForgetTheEggs.Core.ViewModels
{
    public class NewCategoryViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
