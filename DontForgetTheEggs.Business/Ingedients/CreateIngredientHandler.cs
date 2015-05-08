using System.Threading.Tasks;
using DontForgetTheEggs.Business.Categories;
using DontForgetTheEggs.Core.Ingedients;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.Ingedients
{
    public class CreateIngredientHandler : IAsyncRequestHandler<CreateIngredient, int>
    {
        private readonly EggsContext _context;

        public CreateIngredientHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<int> HandleAsync(CreateIngredient request)
        {
            //Get or create CAtegory
            var category = request.CategoryId != null
                ? await CategorySharedActions.GetCategoryAsync(_context, request.CategoryId.Value)
                : CategorySharedActions.CreateCategory(_context, request.NewCategoryName);
            //Create new Ingredient
            var newIngredient = IngredientSharedActions.CreateIngredient(_context, request.Name, category);
            _context.Ingredients.Add(newIngredient);
            await _context.SaveChangesAsync();
            return newIngredient.Id;
        }
    }
}
