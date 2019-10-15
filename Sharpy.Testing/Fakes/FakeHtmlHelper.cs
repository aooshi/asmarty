//using System.Web.Mvc;
//using System.Web.SessionState;

//namespace Sharpy.Testing.Fakes
//{
//    public class FakeHtmlHelper : HtmlHelper
//    {
//        public FakeHtmlHelper() : this(new FakeController(), new FakeView())
//        {
//        }

//        public FakeHtmlHelper(ControllerBase controller, IView view) : this(controller, view, new SessionStateItemCollection())
//        {
//        }

//        public FakeHtmlHelper(ControllerBase controller, IView view, SessionStateItemCollection sessionData) : base(new FakeViewContext(controller, view, sessionData), new FakeViewDataContainer())
//        {
//        }
//    }
//}