using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class CreateGroceryList : IAsyncRequest<int>
    {
        public string Name { get; set; }
    }
}
