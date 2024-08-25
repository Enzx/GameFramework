using System;
using System.Collections.Generic;


namespace ConsoleApp1
{


    public class Component
    {

    }
    class GameObject
    {


        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public void AddComponent<TType>()
        {

            Type type = typeof(TType);
            object obj = Activator.CreateInstance(type);
            _services.Add(type, obj);
        }

        public TType GetComponent<TType>()
        {
            Type type = typeof(TType);
            return (TType)_services[type];
        }
    }
}
