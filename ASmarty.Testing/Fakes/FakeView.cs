using System.IO;
using System.Web.Mvc;

namespace ASmarty.Testing.Fakes
{
    public class FakeView : IView
    {
        public void Render(ViewContext viewContext, TextWriter writer)
        {
        }
    }
}