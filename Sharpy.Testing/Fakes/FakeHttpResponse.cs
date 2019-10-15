using System.Collections.Specialized;
using System.Web;

namespace ASmarty.Testing.Fakes
{
    public class FakeHttpResponse : HttpResponseBase
    {
        private readonly NameValueCollection headers = new NameValueCollection();

        public override int StatusCode { get; set; }

        public override NameValueCollection Headers
        {
            get { return headers; }
        }

        public override void AddHeader(string name, string value)
        {
            headers.Add(name, value);
        }
    }
}