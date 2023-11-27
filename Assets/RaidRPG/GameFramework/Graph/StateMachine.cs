namespace RaidRPG.GameFramework.Graph
{
    //A data structure that represents an unique identifier for a node

    public class StateMachine : State
    {
        private Graph _graph;
        private NodeId _currentState;
        private NodeId _initialState;

        public StateMachine(State initialState)
        {
            _graph = new Graph(initialState);
            _initialState = initialState.Id;
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

        public void AddTransition(NodeId from, NodeId to)
        {
            _graph.AddTransition(from, to);
        }
        


        protected override void OnEnter()
        {
            base.OnEnter();
            _currentState = _initialState;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            Node node = _graph[_currentState];
            Result result = node.Execute();
            if (result == Result.Success)
            {
                Transition transition = _graph.GetTransition(_currentState);
                _currentState = transition.Destination;
            }
            else
            {
                Finish(false);
            }
        }
    }


    public abstract class StateMachine<TAgent> : StateMachine
    {
        private TAgent _agent;

        protected StateMachine(TAgent agent, State initialState) : base(initialState)
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
        
    }
    
    public abstract class State<TAgent> : State
    {
        protected TAgent Agent;
        
        public void SetAgent(TAgent agent)
        {
            Agent = agent;
        }
    }
}