using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class CreateCategory : IAsyncRequest<int>
    {
        public string Name { get; set; }
    }
}
