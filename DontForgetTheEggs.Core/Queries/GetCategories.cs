using System.Collections.Generic;
using DontForgetTheEggs.Core.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Core.Queries
{
    public class GetCategories : IRequest<IEnumerable<CategoryViewModel>>
    {
    }
}
