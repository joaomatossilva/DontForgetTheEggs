using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Core.Data
{
    public class GroceryList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueIn { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? CompletedUtc { get; set; }
        public bool Complete { get; set; }
        public virtual ICollection<GroceryListItem> Items { get; set; }
    }
}
