using System;
using System.IO;

namespace ASmarty.ViewEngine
{
    public interface IView
    {
        void Render(ViewContext viewContext, AccessContext accessContext, TextWriter writer);
    }
}
