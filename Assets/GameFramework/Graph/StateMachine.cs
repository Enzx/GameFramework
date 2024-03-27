using System;

namespace GameFramework.Graph
{
    public class StateMachine : State
    {
        private readonly Graph _graph;
        private NodeId _currentState;
        private readonly NodeId _initialState;

        public StateMachine(State initialState)
        {
            _graph = new Graph(initialState);
            _initialState = initialState.Key;
            _currentState = _initialState;
        }

        public virtual void AddState(State state)
        {
            _graph.AddNode(state);
        }
        
        public void AddCondition(Condition condition)
        {
            _graph.AddNode(condition);
        }

        public void AddTransition(Node from, Node to)
        {
            _graph.AddTransition(from.Key, to.Key);
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            _currentState = _initialState;
        }

        protected override void OnUpdate(float deltaTime)
        {
            Node node = _graph[_currentState];
            Result result = node.Execute();
            switch (result)
            {
                case Result.Success:
                {
                    Transition transition = _graph.GetTransition(_currentState);
                    _currentState = transition.Destination;
                    break;
                }
                case Result.Failure:
                    Finish(false);
                    break;
                case Result.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }


    public class StateMachine<TAgent> : StateMachine
    {
        private readonly TAgent _agent;

        public StateMachine(TAgent agent, State initialState) : base(initialState)
        {
            _agent = agent;
            SetAgent(initialState);
        }

        private void SetAgent(State state)
        {
            //Cast state to State<TAgent> and set agent
            State<TAgent> stateT = (State<TAgent>)state;
            stateT.SetAgent(_agent);
        }

        public override void AddState(State state)
        {
            base.AddState(state);
            SetAgent(state);
        }
        
        public void AddCondition(Condition<TAgent> condition)
        {
            base.AddCondition(condition);
            condition.SetAgent(_agent);
        }
        
    }
}