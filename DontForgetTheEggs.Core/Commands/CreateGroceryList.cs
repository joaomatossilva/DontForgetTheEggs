using System.ComponentModel.DataAnnotations;
using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class CreateGroceryList : IAsyncRequest<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
