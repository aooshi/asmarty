using System.Collections.Generic;

namespace Sharpy.ViewEngine
{
    public interface IFunctionWithContent : INamedExport
    {
        IList<string> TagsToIgnore();
    }
}
