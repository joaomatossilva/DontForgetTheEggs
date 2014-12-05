using System.Linq;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Data.CommandHandlers
{
    public class SetGroceryListCompletedHandler : IRequestHandler<SetGroceryListCompleted, UnitType>
    {
        private readonly EggsContext _context;

        public SetGroceryListCompletedHandler(EggsContext context)
        {
            _context = context;
        }

        public UnitType Handle(SetGroceryListCompleted request)
        {
            var groceryList = _context.GroceryLists.Single(x => x.Id == request.GroceryListId);
            groceryList.Completed = request.Completed;
            _context.SaveChanges();
            return UnitType.Default;
        }
    }
}
