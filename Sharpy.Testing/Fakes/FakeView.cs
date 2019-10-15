using System.IO;
using System.Web.Mvc;

namespace Sharpy.Testing.Fakes
{
    public class FakeView : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
        }
    }
}