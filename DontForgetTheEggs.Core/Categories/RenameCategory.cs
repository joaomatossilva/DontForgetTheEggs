using System.ComponentModel.DataAnnotations;
using ShortBus;

namespace DontForgetTheEggs.Core.Categories
{
    public class RenameCategory : IAsyncRequest<UnitType>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
