using System.Threading.Tasks;
using DontForgetTheEggs.Business.Categories;
using DontForgetTheEggs.Core.Ingedients;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.Ingedients
{
    public class EditIngredientHandler : IAsyncRequestHandler<EditIngredient, UnitType>
    {
        private readonly EggsContext _context;

        public EditIngredientHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<UnitType> HandleAsync(EditIngredient request)
        {
            //Get or create Category
            var category = request.CategoryId != null
                ? await CategorySharedActions.GetCategoryAsync(_context, request.CategoryId.Value)
                : CategorySharedActions.CreateCategory(_context, request.NewCategoryName);

            var ingredient = await _context.Ingredients.FindAsync(request.IngredientId);
            ingredient.Category = category;
            ingredient.Name = request.Name;
            await _context.SaveChangesAsync();
            return UnitType.Default;
        }
    }
}
