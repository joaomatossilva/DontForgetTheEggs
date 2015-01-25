using System.Data.Entity;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Business.Shared;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.CommandHandlers
{
    public class AddIngredientOnGorceryListHander : IAsyncRequestHandler<AddIngredientOnGorceryList, UnitType>
    {
        private readonly EggsContext _context;

        public AddIngredientOnGorceryListHander(EggsContext context)
        {
            _context = context;
        }

        public async Task<UnitType> HandleAsync(AddIngredientOnGorceryList request)
        {
            //If there is an Ingredient already on the list, we increment the Quantity.
            var existingGrocery = await _context.Groceries
                .SingleOrDefaultAsync(g => g.Ingredient.Id == request.IngredientId && g.GroceryList.Id == request.GroceryListId);
            if (existingGrocery != null)
            {
                existingGrocery.Quanity += request.Quantity;
                return UnitType.Default;
            }

            //Get Ingredient
            var newIngredient = await _context.Ingredients.FindAsync(request.IngredientId);
            //Create Grocery for the Ingredient and add it to the list
            var groceryList = await _context.GroceryLists.FindAsync(request.GroceryListId);
            groceryList.Groceries.Add(new Grocery { Ingredient = newIngredient, Quanity = request.Quantity});
            await _context.SaveChangesAsync();
            return UnitType.Default;
        }
    }
}
