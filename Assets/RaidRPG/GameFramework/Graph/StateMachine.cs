using System;
using System.Collections.Generic;

namespace RaidRPG.GameFramework.Graph
{
    public enum Status
    {
        Enter,
        Update,
        Exit
    }

    public enum Result
    {
        None,
        Success,
        Failure
    }

    public enum ExecuteMode
    {
        Parallel,
        Sequence
    }

    public class Graph
    {
        //Transitions are edges between nodes
        private List<Transition> _transitions;
        private Dictionary<NodeId, Node> _nodes;
        private Node _startNode;

        public Graph(Node startNode)
        {
            _transitions = new List<Transition>();
            _nodes = new Dictionary<NodeId, Node>();
            _startNode = startNode;
        }


        public void AddNode(Node node)
        {
            _nodes.Add(node.Id, node);
        }

//Create a transition from a node to another and store it
        public void AddTransition(NodeId from, NodeId to)
        {
            Transition transition = new(from, to);
            _transitions.Add(transition);
        }

        public Node this[NodeId currentState] => _nodes[currentState];

        public Transition GetTransition(NodeId currentState)
        {
            for (int index = 0; index < _transitions.Count; index++)
            {
                Transition transition = _transitions[index];
                if (transition.Source.Equals(currentState))
                {
                    return transition;
                }
            }

            throw new Exception("Transition not found");
        }
    }

//A data structure that represents an unique identifier for a node
    public struct NodeId
    {
    }

    public abstract class Node
    {
        public NodeId Id;
        public abstract Result Execute();
    }

    public struct Transition
    {
        public readonly NodeId Source;
        public readonly NodeId Destination;

        public Transition(NodeId from, NodeId to)
        {
            Source = from;
            Destination = to;
        }
    }

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

    public abstract class ActionTask
    {
        public abstract Result Execute();
    }

    public abstract class ConditionTask
    {
        public abstract bool Check();
    }


    public  class Condition : Node
    {
        public ExecuteMode ExecuteMode;
        private List<ConditionTask> _conditions;
        private Result _result;

        public Condition()
        {
            _conditions = new List<ConditionTask>();
        }

        public override Result Execute()
        {
            _result = Result.None;
            bool success = ExecuteMode switch
            {
                ExecuteMode.Parallel => Parallel(),
                ExecuteMode.Sequence => Sequence(),
                _ => throw new ArgumentOutOfRangeException()
            };

            Finish(success);
            return _result;
        }
        

        private void Finish(bool success)
        {
            _result = success ? Result.Success : Result.Failure;
        }

        private bool Parallel()
        {
            bool success = true;
            for (int index = 0; index < _conditions.Count; index++)
            {
                ConditionTask condition = _conditions[index];
                if (!condition.Check())
                {
                    success = false;
                }
            }

            return success;
        }

        private bool Sequence()
        {
            bool success = true;
            for (int index = 0; index < _conditions.Count; index++)
            {
                ConditionTask condition = _conditions[index];
                if (!condition.Check())
                {
                    success = false;
                    break;
                }
            }

            return success;
        }
    }


    public  class State : Node
    {
        private Status _status;
        private Result _result;
        private List<ActionTask> _actions;

        public override Result Execute()
        {
            switch (_status)
            {
                case Status.Enter:
                    Enter();
                    break;
                case Status.Update:
                    Update();
                    break;
                case Status.Exit:
                    Exit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return _result;
        }


        private void Enter()
        {
            _status = Status.Enter;
            _result = Result.None;
            OnEnter();
        }

        private void Update()
        {
            _status = Status.Update;
            OnUpdate();
        }

        private void Exit()
        {
            _status = Status.Exit;
            OnExit();
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnExit()
        {
        }

        protected void Finish(bool success)
        {
            _result = success ? Result.Success : Result.Failure;
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