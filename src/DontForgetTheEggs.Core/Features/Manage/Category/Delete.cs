using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DontForgetTheEggs.Core.Common.Exceptions;
using DontForgetTheEggs.Core.Data;
using FluentValidation;
using MediatR;

namespace DontForgetTheEggs.Core.Features.Manage.Category
{
    public static class Delete
    {
        public class Query : IRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IRequest
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int? ExistingNewCategoryId { get; set; }

            public bool HasGroceries { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly EggsDbContext dbContext;

            public CommandValidator(EggsDbContext dbContext)
            {
                this.dbContext = dbContext;
                RuleFor(x => x.ExistingNewCategoryId).NotNull().WhenAsync(async (x, token) =>
                    await this.dbContext.Categories.HasGroceries(x.Id, token)
                    .ConfigureAwait(false));
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
                var category = await _dbContext.Categories
                    .Where(x => x.Id == request.Id)
                   .ProjectToFirstOrDefaultAsync<Command>();

                if (category == null)
                {
                    throw new EggsDataException("Category not found");
                }

                category.HasGroceries = await _dbContext.Categories.HasGroceries(request.Id, cancellationToken)
                    .ConfigureAwait(false);

                return category;
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
                if(request.ExistingNewCategoryId != null)
                {
                    var groceries = await _dbContext.Groceries
                        .Where(x => x.Category.Id == request.Id)
                        .ToListAsync(cancellationToken)
                        .ConfigureAwait(false);
                    foreach(var grocery in groceries)
                    {
                        grocery.CategoryId = (int)request.ExistingNewCategoryId;
                    }
                }

                var category = new Data.Category { Id = request.Id };
                _dbContext.Categories.Attach(category);
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync(cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}
