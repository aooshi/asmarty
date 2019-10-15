using System.Collections.Specialized;
using System.Web;

namespace ASmarty.Testing.Fakes
{
    public class FakeHttpRequest : HttpRequestBase
    {
        private readonly NameValueCollection formParams;
        private readonly NameValueCollection queryStringParams;
        private readonly HttpCookieCollection cookies;

        public FakeHttpRequest(NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies)
        {
            this.formParams = formParams;
            this.queryStringParams = queryStringParams;
            this.cookies = cookies;
        }

        public override NameValueCollection Form
        {
            get
            {
                return formParams;
            }
        }

        public override NameValueCollection QueryString
        {
            get
            {
                return queryStringParams;
            }
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return cookies;
            }
        }

    }
}