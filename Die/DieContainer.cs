using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MyDie.Die.Lib;

namespace MyDie.Die
{
    public sealed class DieContainer : IDieContainer
    {
        private readonly IDictionary<Type, object> _singletons = new Dictionary<Type, object>();
        private readonly IDictionary<Type, DieFactory<object>> _factories = new Dictionary<Type, DieFactory<object>>();

        public T Get<T>() where T : class => Get(typeof(T)) as T;

        public object Get(Type type)
        {
            if (_singletons.TryGetValue(type, out var singleton))
                return singleton;

            if (_factories.TryGetValue(type, out var factory))
            {
                var instance = factory.Invoke(this);
                var config = instance.GetType().GetCustomAttribute<DieAddictionAttribute>();
                if (config?.DieLifecycle == DieLifecycle.Singleton)
                    _singletons.Add(type, instance);

                return instance;
            }

            return null;
        }

        public void Register<TInter, TImpl>() where TInter : class where TImpl : class, TInter =>
            Register(typeof(TInter), typeof(TImpl));

        public void Register<T>(DieFactory<T> factory) where T : class =>
            Register(typeof(T), factory);

        public void Register(Assembly assembly)
        {
            foreach (var impl in assembly.GetTypes())
            {
                var configs = impl.GetCustomAttribute<DieAddictionAttribute>();
                if (configs == null)
                    continue;

                var interfaces = impl.GetInterfaces();
                if (interfaces.Any())
                {
                    foreach (var inter in interfaces)
                        Register(inter, impl);

                    continue;
                }

                Register(impl, impl);
            }
        }

        private void Register(Type inter, Type impl) =>
            Register(inter, c => Instantiate(impl, c));

        private void Register(Type type, DieFactory<object> factory) =>
            _factories.Add(type, factory);

        private static object Instantiate(Type type, IDieContainer container)
        {
            var constructorInfo = type.GetConstructors().Single();
            var parameters = constructorInfo
                .GetParameters()
                .Select(parameter => parameter.ParameterType)
                .Select(container.Get)
                .ToArray();

            return constructorInfo.Invoke(parameters);
        }
    }
}
