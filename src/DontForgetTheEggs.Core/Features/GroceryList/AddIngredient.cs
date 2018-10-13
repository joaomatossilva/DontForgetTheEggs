using DontForgetTheEggs.Core.Data;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Core.Features.GroceryList
{
    public static class AddIngredient
    {
        public class Command : IRequest
        {
            public int GroceryListId { get; set; }
            public int Quantity { get; set; }
            public int? GroceryId { get; set; }
        }

        public class CommandValidation : AbstractValidator<Command>
        {
            public CommandValidation()
            {
                RuleFor(x => x.GroceryId).NotNull();
                //TODO: Add validation if already Exists
            }
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
                var newListItem = new GroceryListItem
                {
                    GroceryId = (int)request.GroceryId,
                    Quantity = request.Quantity,
                    GroceryListId = request.GroceryListId
                };
                _dbContext.GroceryListItems.Add(newListItem);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
