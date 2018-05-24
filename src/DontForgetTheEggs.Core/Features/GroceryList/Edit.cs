using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Common.Exceptions;
using DontForgetTheEggs.Core.Data;
using MediatR;
using AutoMapper;

namespace DontForgetTheEggs.Core.Features.GroceryList
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
            public DateTime DueIn { get; set; }
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
                var groceryList = await  _dbContext.GroceryLists
                    .Where(x => x.Id == request.Id)
                   .ProjectToFirstOrDefaultAsync<Command>();

                if(groceryList == null)
                {
                    throw new EggsDataException("Grocery List not found");
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
                var groceryList = await _dbContext.GroceryLists
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (groceryList == null)
                {
                    throw new EggsDataException("Grocery List not found");
                }

                groceryList.Name = request.Name;
                groceryList.DueIn = request.DueIn;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
