using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Common.Exceptions;
using DontForgetTheEggs.Core.Data;
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
                var category = await FindOrCreateCategory(request.CategoryId, request.CategoryName, cancellationToken)
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

            private async Task<Category> FindOrCreateCategory(int? id, string name, CancellationToken cancellationToken)
            {
                Category category;

                //find by id
                if(id != null)
                {
                    category = await _dbContext.Categories
                        .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                        .ConfigureAwait(false);

                    if (category == null)
                    {
                        throw new EggsDataException("Unable to get the category");
                    }

                    return category;
                }

                //try to find by name first before creating one
                category = await _dbContext.Categories
                        .FirstOrDefaultAsync(x => x.Name == name, cancellationToken)
                        .ConfigureAwait(false);
                
                if(category == null)
                {
                    category = new Category
                    {
                        Name = name
                    };
                }

                return category;
            }

        }
    }
}
