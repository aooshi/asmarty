using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;

namespace ASmarty.Testing.Fakes
{
    public class FakeHttpContext : HttpContextBase
    {
        private readonly FakePrincipal principal;
        private readonly NameValueCollection formParams;
        private readonly NameValueCollection queryStringParams;
        private readonly HttpCookieCollection cookies;
        private readonly SessionStateItemCollection sessionItems;
        private readonly FakeHttpResponse httpResponse;


        public FakeHttpContext(FakePrincipal principal, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies, SessionStateItemCollection sessionItems )
        {
            this.principal = principal;
            this.formParams = formParams;
            this.queryStringParams = queryStringParams;
            this.cookies = cookies;
            this.sessionItems = sessionItems;
            httpResponse = new FakeHttpResponse();
        }

        public override HttpRequestBase Request
        {
            get
            {
                return new FakeHttpRequest(formParams, queryStringParams, cookies);
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return httpResponse;
            }
        }

        public override IPrincipal User
        {
            get
            {
                return principal;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override HttpSessionStateBase Session
        {
            get
            {
                return new FakeHttpSessionState(sessionItems);
            }
        }

    }
}