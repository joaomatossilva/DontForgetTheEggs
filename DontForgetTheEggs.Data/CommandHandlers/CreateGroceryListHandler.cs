using System.Threading.Tasks;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.CommandHandlers
{
    public class CreateGroceryListHandler : IAsyncRequestHandler<CreateGroceryList, int>
    {
        private readonly EggsContext _context;

        public CreateGroceryListHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<int> HandleAsync(CreateGroceryList request)
        {
            var newGroceryList = new GroceryList
            {
                Name = request.Name
            };
            _context.GroceryLists.Add(newGroceryList);
            await _context.SaveChangesAsync();
            return newGroceryList.Id;
        }
    }
}
