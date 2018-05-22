using System;
using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Data;
using FluentValidation;
using MediatR;

namespace DontForgetTheEggs.Core.Features.GroceryList
{
    public static class Create
    {
        public class Command : IRequest<int>
        {
            public Command()
            {
                //TODO: move this logic to a query
                DueIn = DateTime.Today.AddDays(1);
            }

            public string Name { get; set; }
            public DateTime DueIn { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotNull();
            }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly EggsDbContext _dbContext;

            public CommandHandler(EggsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var newList = new Data.GroceryList
                {
                    Name = request.Name,
                    DueIn = request.DueIn,
                    CreatedUtc = DateTime.UtcNow
                };

                _dbContext.GroceryLists.Add(newList);
                await _dbContext.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return newList.Id;
            }
        }
    }
}
