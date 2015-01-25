using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.CommandHandlers
{
    public class SetGroceryListCompletedHandler : IAsyncRequestHandler<SetGroceryListCompleted, UnitType>
    {
        private readonly EggsContext _context;

        public SetGroceryListCompletedHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<UnitType> HandleAsync(SetGroceryListCompleted request)
        {
            var groceryList = await _context.GroceryLists.SingleAsync(x => x.Id == request.GroceryListId);
            groceryList.Completed = request.Completed;
            await _context.SaveChangesAsync();
            return UnitType.Default;
        }
    }
}
