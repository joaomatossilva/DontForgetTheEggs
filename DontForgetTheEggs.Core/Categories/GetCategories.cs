using System.Collections.Generic;
using DontForgetTheEggs.Core.Categories.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Core.Categories
{
    public class GetCategories : IAsyncRequest<IEnumerable<CategoryViewModel>>
    {
    }
}
