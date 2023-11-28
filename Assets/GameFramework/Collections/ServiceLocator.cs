using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFramework.Collections
{
    public class ServiceLocator<TType>
    {
        private readonly Dictionary<Type, TType> _behaviours;

        private FixedTypeKeyHashtable<TType> _behaviorTypeMap;

        public ServiceLocator(int count = 0)
        {
            _behaviours = new Dictionary<Type, TType>(count);
        }


        private void Register(Type type, TType instance)
        {
            _behaviours.Add(type, instance);
        }
    
        public void Register(TType instance)
        {
            Type type = instance.GetType();
            Register(type, instance);
        }

        public void Register<TInterface>(TType instance)
        {
            Type type = typeof(TInterface);
            Register(type, instance);
        }

        public void Build()
        {
            _behaviorTypeMap = new FixedTypeKeyHashtable<TType>(_behaviours.ToArray());
        }

        public T Get<T>() where T : class, TType
        {
            return _behaviorTypeMap.Get(typeof(T)) as T;
        }
        
        public void ForEach(Action<TType> action)
        {
            _behaviorTypeMap.ForEach(action);
        }
    }
}