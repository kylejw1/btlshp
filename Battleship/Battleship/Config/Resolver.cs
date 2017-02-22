using System;
using System.Collections.Generic;

namespace Battleship.Config
{
    public static class Resolver
    {
        private static Dictionary<Type, object> _registeredObjects = new Dictionary<Type, object>();

        /// <summary>
        /// Register object as the single-instance dependency by type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="obj"></param>
        public static void RegisterObject(Type type, object obj)
        {
            _registeredObjects[type] = obj;
        }

        /// <summary>
        /// Resolve defined dependencies based on object type
        /// </summary>
        /// <typeparam name="TResolved"></typeparam>
        /// <returns></returns>
        public static TResolved Resolve<TResolved>()
        {
            var type = typeof(TResolved);
            if (!_registeredObjects.ContainsKey(type))
            {
                throw new KeyNotFoundException(string.Format("Resolver :: Resolve :: No dependency for type {0} exists.", type.ToString()));
            }

            return (TResolved)_registeredObjects[type];
        }
    }
}
