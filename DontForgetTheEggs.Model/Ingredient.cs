using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Model
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
    }
}
