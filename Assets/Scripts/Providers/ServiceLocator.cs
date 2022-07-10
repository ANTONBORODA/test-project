using System;
using System.Collections.Generic;

namespace Providers
{
    /// <summary>
    /// Generally, I would avoid using service locator and use dependency injection framework like Zenject, but for
    /// this demo purposes primitive locator will do 
    /// </summary>
    public class ServiceLocator : IServiceLocator
    {
        public static IServiceLocator Instance;
        private readonly Dictionary<Type, object> _services = new();
        
        public ServiceLocator()
        {
            Instance = this;
        }
        
        public void AddService(Type serviceType, object service)
        {
            if (_services.ContainsKey(serviceType)) throw new InvalidOperationException("Service already registered!");
            _services.Add(serviceType, service);
        }

        public T GetService<T>() where T : class
        {
            if (!_services.ContainsKey(typeof(T))) throw new InvalidOperationException("Service not registered!");
            return _services[typeof(T)] as T;
        }
    }

    public interface IServiceLocator
    {
        T GetService<T>() where T : class;
    }
    
}