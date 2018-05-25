using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Data;
using FluentValidation;
using MediatR;

namespace DontForgetTheEggs.Core.Features.Manage.Category
{
    public static class Create
    {
        public class Command : IRequest<int>
        {
            public string Name { get; set; }
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
                var category = new Data.Category
                {
                    Name = request.Name
                };

                _dbContext.Categories.Add(category);
                await _dbContext.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);

                return category.Id;
            }
        }
    }
}
