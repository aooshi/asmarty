using System.Collections.Generic;

namespace ASmarty.ViewEngine
{
    public interface IFunctionWithContent : INamedExport
    {
        IList<string> TagsToIgnore();
    }
}
