using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Common.Exceptions;
using DontForgetTheEggs.Core.Data;
using MediatR;
using AutoMapper;
using System.Collections.Generic;

namespace DontForgetTheEggs.Core.Features.GroceryList
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
            public DateTime DueIn { get; set; }

            public List<IngredientItemModel> Items { get; set; }

        }

        public class IngredientItemModel
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            public bool Checked { get; set; }
            public DateTime? CheckedDatedUtc { get; set; }
            public int GroceryId { get; set; }
            public string GroceryName { get; set; }
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
                var groceryList = await  _dbContext.GroceryLists
                    .Where(x => x.Id == request.Id)
                   .ProjectToFirstOrDefaultAsync<Model>();

                if(groceryList == null)
                {
                    throw new EggsDataException("Grocery List not found");
                }

                return groceryList;
            }
        }
        
    }
}
