using System.Linq;
using System.Security.Principal;

namespace ASmarty.Testing.Fakes
{
    public class FakePrincipal : IPrincipal
    {
        private readonly IIdentity identity;
        private readonly string[] roles;

        public FakePrincipal(IIdentity identity, string[] roles)
        {
            this.identity = identity;
            this.roles = roles;
        }

        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string role)
        {
            if (roles == null)
                return false;
            return roles.Contains(role);
        }
    }
}