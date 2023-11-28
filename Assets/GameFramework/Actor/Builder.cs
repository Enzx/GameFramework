using System;
using GameFramework.Actor.Behaviours;
using GameFramework.Actor.DataModel;
using GameFramework.Collections;

namespace GameFramework.Actor
{
    public class Builder : IDataVisitor
    {
        private readonly IBehaviour _agent;
        private readonly ServiceLocator<IBehaviour> _serviceLocator;
        public Builder(IBehaviour agent, ServiceLocator<IBehaviour> serviceLocator)
        {
            _agent = agent;
            _serviceLocator = serviceLocator;
        }
    

        public IBehaviour Visit<TBehavior>(IData data)
        {
            Type dataType = typeof(TBehavior);
            IBehaviour behaviour = (IBehaviour)Activator.CreateInstance(dataType, _agent, data);
            _serviceLocator.Register(behaviour);
            return behaviour;
        }

        public IBehaviour Visit<TBehavior, TInterface>(IData data)
        {
            IBehaviour behaviour = Visit<TBehavior>(data);
            _serviceLocator.Register<TInterface>(behaviour);
            return behaviour;
        }
    }
}