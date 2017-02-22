using Battleship.Logging;
using Battleship.PlayerInterface;
using Battleship.View;
using System;
using System.Collections.Generic;

namespace Battleship.Config
{
    public static class Resolver
    {
        private static Dictionary<Type, object> _registeredObjects = new Dictionary<Type, object>();

        static Resolver()
        {
            // All resolved interface instances can easily be made configurable in a config file, or even at run time.  
            // Eg. for hypothetical network play, the console could be used to determine network configuration, host or client, etc. 
            // Then the game command provider would use some kind of network stream instead of std I/O.
            // Though a complete game would likely use a package such as castle windsor to achieve this.
            Resolver.RegisterObject(typeof(ILogger), new Log4NetLogger());
            Resolver.RegisterObject(typeof(IGameView), new ConsoleGameView());
            // Configuration provider could take the form of a UI+Controller, Web App, etc.  For this demo, it uses Console + Std I/O
            Resolver.RegisterObject(typeof(IConfigurationProvider), new TextConfiguratonProvider(Console.In, Console.Out));
            // Player command provider could take the form of an AI player, TCP socket for network play, UI+Controller, etc.  
            Resolver.RegisterObject(typeof(IPlayerInterface), new TextPlayerInterface(Console.In, Console.Out));
        }

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
