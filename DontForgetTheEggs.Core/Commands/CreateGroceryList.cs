using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class CreateGroceryList : IRequest<int>
    {
        public string Name { get; set; }
    }
}
