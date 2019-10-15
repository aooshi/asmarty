using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;

namespace Sharpy.Testing.Fakes
{
    public class FakeHttpSessionState : HttpSessionStateBase
    {
        private readonly SessionStateItemCollection sessionItems;

        public FakeHttpSessionState(SessionStateItemCollection sessionItems)
        {
            this.sessionItems = sessionItems;
        }

        public override void Add(string name, object value)
        {
            sessionItems[name] = value;
        }

        public override int Count
        {
            get
            {
                return sessionItems.Count;
            }
        }

        public override IEnumerator GetEnumerator()
        {
            return sessionItems.GetEnumerator();
        }

        public override NameObjectCollectionBase.KeysCollection Keys
        {
            get
            {
                return sessionItems.Keys;
            }
        }

        public override object this[string name]
        {
            get
            {
                return sessionItems[name];
            }
            set
            {
                sessionItems[name] = value;
            }
        }

        public override object this[int index]
        {
            get
            {
                return sessionItems[index];
            }
            set
            {
                sessionItems[index] = value;
            }
        }

        public override void Remove(string name)
        {
            sessionItems.Remove(name);
        }
    }
}