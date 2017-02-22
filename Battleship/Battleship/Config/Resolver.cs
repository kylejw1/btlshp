using System;
using System.Collections.Generic;

namespace Battleship.Config
{
    public static class Resolver
    {
        private static Dictionary<Type, object> _registeredObjects = new Dictionary<Type, object>();

        public static void RegisterObject(Type type, object obj)
        {
            _registeredObjects[type] = obj;
        }

        public static TResolved Resolve<TResolved>()
        {
            var type = typeof(TResolved);
            if (!_registeredObjects.ContainsKey(type))
            {
                throw new KeyNotFoundException(string.Format("No dependency for type {0} exists.", type.ToString()));
            }

            return (TResolved)_registeredObjects[type];
        }
    }
}
