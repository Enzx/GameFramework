using System;
using System.Collections.Generic;

namespace GameFramework.Graph
{
    public abstract class State<TAgent> : State
    {
        protected TAgent Agent;

        public override void AddAction(ActionTask action)
        {
            if (action is not ActionTask<TAgent> agentAction)
            {
                throw new ArgumentException($"Action {action} is not of type {typeof(TAgent)}");
            }

            agentAction.SetAgent(Agent);

            base.AddAction(action);
        }

        public void SetAgent(TAgent agent)
        {
            Agent = agent;
        }
    }

    public class State : Node
    {
        private Status _status;
        private Result _result;
        private List<ActionTask> _actions = new();
        public float DeltaTime;

        public virtual void AddAction(ActionTask action)
        {
            _actions.Add(action);
        }

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
            _actions.ForEach(action => action.Execute());
            OnEnter();
            _status = Status.Update;
        }

        private void Update()
        {
            _actions.ForEach(action => action.Update(DeltaTime));
            OnUpdate(DeltaTime);
        }

        private void Exit()
        {
            OnExit();
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnUpdate(float deltaTime)
        {
        }

        protected virtual void OnExit()
        {
        }

        protected void Finish(bool success)
        {
            _result = success ? Result.Success : Result.Failure;
            _status = Status.Exit;
        }
    }
}