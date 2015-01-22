using System.Threading.Tasks;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Business.Shared;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.CommandHandlers
{
    public class AddNewIngredientOnGorceryListHander : IAsyncRequestHandler<AddNewIngredientOnGorceryList, UnitType>
    {
        private readonly EggsContext _context;

        public AddNewIngredientOnGorceryListHander(EggsContext context)
        {
            _context = context;
        }

        public async Task<UnitType> HandleAsync(AddNewIngredientOnGorceryList request)
        {
            //Get or create CAtegory
            var category = request.CategoryId != null
                ? await CategorySharedActions.GetCategoryAsync(_context, request.CategoryId.Value)
                : CategorySharedActions.CreateCategory(_context, request.CategoryName);
            //Create new Ingredient
            var newIngredient = IngredientSharedActions.CreateIngredient(_context, request.IngredientName, category);
            //Create Grocery for the Ingredient and add it to the list
            var groceryList = await _context.GroceryLists.FindAsync(request.GroceryListId);
            groceryList.Groceries.Add(new Grocery { Ingredient = newIngredient, Quanity = request.Quantity});
            await _context.SaveChangesAsync();
            return UnitType.Default;
        }
    }
}
