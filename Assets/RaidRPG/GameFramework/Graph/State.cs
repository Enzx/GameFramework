using System;
using System.Collections.Generic;

namespace RaidRPG.GameFramework.Graph
{
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
}