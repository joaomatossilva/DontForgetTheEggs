using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Common.Exceptions;
using DontForgetTheEggs.Core.Data;
using MediatR;
using AutoMapper;
using FluentValidation;

namespace DontForgetTheEggs.Core.Features.Manage.Category
{
    public class Edit
    {
        public class Query : IRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly EggsDbContext _dbContext;

            public QueryHandler(EggsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Command> Handle(Query request, CancellationToken cancellationToken)
            {
                var groceryList = await  _dbContext.Categories
                    .Where(x => x.Id == request.Id)
                   .ProjectToFirstOrDefaultAsync<Command>();

                if(groceryList == null)
                {
                    throw new EggsDataException("Category not found");
                }

                return groceryList;
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
                var groceryList = await _dbContext.Categories
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (groceryList == null)
                {
                    throw new EggsDataException("Category not found");
                }

                groceryList.Name = request.Name;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
