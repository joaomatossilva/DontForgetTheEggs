using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Data;
using DontForgetTheEggs.Core.Features.Manage.Category;
using MediatR;

namespace DontForgetTheEggs.Core.Features.GroceryList
{
    public class AddNewIngredient
    {
        public class Command : IRequest
        {
            public int GroceryListId { get; set; }
            public int Quantity { get; set; }
            public string Name { get; set; }
            public string CategoryName { get; set; }
            public int? CategoryId { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly EggsDbContext _dbContext;

            public CommandHandler(EggsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var category = await _dbContext.Categories.FindOrCreateCategory(request.CategoryId, request.CategoryName, cancellationToken)
                    .ConfigureAwait(false);

                var newIngredient = new Grocery
                {
                    Name = request.Name,
                    Category = category
                };

                _dbContext.Groceries.Add(newIngredient);

                var newListItem = new GroceryListItem
                {
                    Grocery = newIngredient,
                    Quantity = request.Quantity,
                    GroceryListId = request.GroceryListId
                };
                _dbContext.GroceryListItems.Add(newListItem);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
