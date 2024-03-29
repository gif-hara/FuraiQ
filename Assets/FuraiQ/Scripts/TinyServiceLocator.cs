using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

namespace FuraiQ
{
    public class TinyServiceLocator
    {
        private static readonly Dictionary<Type, (object service, CancellationTokenSource scope)> services = new();

        private static readonly Dictionary<Type, Dictionary<string, (object service, CancellationTokenSource scope)>> namedServices = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            services.Clear();
            namedServices.Clear();
        }

        public static async UniTaskVoid RegisterAsync<T>(T service, CancellationToken cancellationToken = default)
        {
            if (services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Service already registered: {typeof(T)}");
                return;
            }
            var scope = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            services[typeof(T)] = (service, scope);
            await scope.Token.ToUniTask().Item1;
            RemoveInternal<T>();
        }

        public static async UniTaskVoid RegisterAsync<T>(string name, T service, CancellationToken cancellationToken = default)
        {
            if (!namedServices.TryGetValue(typeof(T), out var namedService))
            {
                namedService = new Dictionary<string, (object service, CancellationTokenSource scope)>();
                namedServices.Add(typeof(T), namedService);
            }

            if (namedService.ContainsKey(name))
            {
                Debug.LogError($"Service already registered: {typeof(T)}, name: {name}");
                return;
            }
            var scope = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            namedService[name] = (service, scope);
            await scope.Token.ToUniTask().Item1;
            RemoveInternal<T>(name);
        }

        public static void Register<T>(T service)
        {
            RegisterAsync(service).Forget();
        }

        public static T Resolve<T>()
        {
            return (T)services[typeof(T)].service;
        }

        public static T Resolve<T>(string name)
        {
            Assert.IsTrue(namedServices.ContainsKey(typeof(T)), $"Service not found: {typeof(T)}");
            Assert.IsTrue(namedServices[typeof(T)].ContainsKey(name), $"Service not found: {typeof(T)}");
            return (T)namedServices[typeof(T)][name].service;
        }

        public static T TryResolve<T>()
        {
            return services.ContainsKey(typeof(T)) ? (T)services[typeof(T)].service : default;
        }

        public static void Remove<T>()
        {
            RemoveInternal<T>();
        }

        public static void Remove<T>(string name)
        {
            RemoveInternal<T>(name);
        }

        private static void RemoveInternal<T>()
        {
            if (services.ContainsKey(typeof(T)))
            {
                services[typeof(T)].scope.Cancel();
                services.Remove(typeof(T));
            }
        }

        private static void RemoveInternal<T>(string name)
        {
            if (namedServices.ContainsKey(typeof(T)))
            {
                if (namedServices[typeof(T)].ContainsKey(name))
                {
                    namedServices[typeof(T)][name].scope.Cancel();
                    namedServices[typeof(T)].Remove(name);
                }
            }
        }
    }
}
