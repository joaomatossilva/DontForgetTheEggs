using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Data.Shared;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Data.CommandHandlers
{
    public class AddNewIngredientOnGorceryListHander : IRequestHandler<AddNewIngredientOnGorceryList, UnitType>
    {
        private readonly EggsContext _context;

        public AddNewIngredientOnGorceryListHander(EggsContext context)
        {
            _context = context;
        }

        public UnitType Handle(AddNewIngredientOnGorceryList request)
        {
            //Get or create CAtegory
            var category = request.CategoryId != null
                ? CategorySharedActions.GetCategory(_context, request.CategoryId.Value)
                : CategorySharedActions.CreateCategory(_context, request.CategoryName);
            //Create new Ingredient
            var newIngredient = IngredientSharedActions.CreateIngredient(_context, request.IngredientName, category);
            //Create Grocery for the Ingredient and add it to the list
            var groceryList = _context.GroceryLists.Find(request.GroceryListId);
            groceryList.Groceries.Add(new Grocery { Ingredient = newIngredient, Quanity = request.Quantity});
            _context.SaveChanges();
            return UnitType.Default;
        }
    }
}
