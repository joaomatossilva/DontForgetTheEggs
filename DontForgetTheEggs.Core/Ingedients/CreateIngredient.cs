using System.ComponentModel.DataAnnotations;
using ShortBus;

namespace DontForgetTheEggs.Core.Ingedients
{
    public class CreateIngredient : IAsyncRequest<int>
    {
        [Required]
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public string NewCategoryName { get; set; }
    }
}
