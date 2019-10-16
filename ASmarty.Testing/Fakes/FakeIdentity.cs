using System;
using System.Security.Principal;

namespace ASmarty.Testing.Fakes
{
    public class FakeIdentity : IIdentity
    {
        private readonly string name;

        public FakeIdentity(string userName)
        {
            name = userName;
        }

        public string AuthenticationType
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsAuthenticated
        {
            get { return !String.IsNullOrEmpty(name); }
        }

        public string Name
        {
            get { return name; }
        }

    }
}