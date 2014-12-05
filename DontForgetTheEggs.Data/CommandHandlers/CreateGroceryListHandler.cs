using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Data.CommandHandlers
{
    public class CreateGroceryListHandler : IRequestHandler<CreateGroceryList, int>
    {
        private readonly EggsContext _context;

        public CreateGroceryListHandler(EggsContext context)
        {
            _context = context;
        }

        public int Handle(CreateGroceryList request)
        {
            var newGroceryList = new GroceryList
            {
                Name = request.Name
            };
            _context.GroceryLists.Add(newGroceryList);
            _context.SaveChanges();
            return newGroceryList.Id;
        }
    }
}
