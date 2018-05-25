using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Common.Exceptions;
using DontForgetTheEggs.Core.Data;
using MediatR;
using AutoMapper;
using System.Collections.Generic;

namespace DontForgetTheEggs.Core.Features.Manage.Category
{
    public class Detail
    {
        public class Query : IRequest<Model>
        {
            public int Id { get; set; }
        }

        public class Model
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<GroceryItemModel> Groceries { get; set; }
        }

        public class GroceryItemModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {
            private readonly EggsDbContext _dbContext;

            public QueryHandler(EggsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await  _dbContext.Categories
                    .Where(x => x.Id == request.Id)
                   .ProjectToFirstOrDefaultAsync<Model>();

                if(category == null)
                {
                    throw new EggsDataException("Category not found");
                }

                return category;
            }
        }
        
    }
}
