using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class RenameCategory : IAsyncRequest<UnitType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
