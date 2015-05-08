using System.ComponentModel.DataAnnotations;
using ShortBus;

namespace DontForgetTheEggs.Core.Categories
{
    public class CreateCategory : IAsyncRequest<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
