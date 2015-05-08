using System.ComponentModel.DataAnnotations;
using ShortBus;

namespace DontForgetTheEggs.Core.Groceries
{
    public class CreateGroceryList : IAsyncRequest<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
