using System;

namespace ASmarty.Tools
{
    internal interface IAccessor
    {
        void Invoke(object instance,object[] args);
    }
    internal class Accessor<T> : IAccessor
    {
        private Action<T, object[]> action = null;
        private Type actionType = typeof(Action<T, object[]>);
        //
        public Accessor(Type type, string methodName)
        {
            var mn = type.GetMethod(methodName);
            if (mn == null)
                throw new InvalidOperationException("type " + type.FullName + " not method " + methodName);

            //
            this.action = (Action<T, object[]>)Delegate.CreateDelegate(actionType, mn);
        }

        public void Invoke(object instance, object[] args)
        {
            this.action((T)instance, args);
        }
    }
}
