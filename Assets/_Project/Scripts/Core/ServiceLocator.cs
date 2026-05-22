using System;
using System.Collections.Generic;

namespace InventixGames.Core
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new();
        public static void Register<T>(T service) where T : class => Services[typeof(T)] = service;
        public static T Get<T>() where T : class
        {
            if (Services.TryGetValue(typeof(T), out var s)) return s as T;
            throw new InvalidOperationException($"Service {typeof(T)} not registered. Did GameBootstrap run?");
        }
        public static bool TryGet<T>(out T service) where T : class
        {
            if (Services.TryGetValue(typeof(T), out var s)) { service = s as T; return service != null; }
            service = null; return false;
        }
        public static void Clear() => Services.Clear();
    }
}
