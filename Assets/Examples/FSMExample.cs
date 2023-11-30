using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Examples
{
    public class Door
    {
        private class Open : State<Door>
        {
            private float _elapsed;

            public Open(Door door) : base(door)
            {

            }

            protected override void OnEnter()
            {
                Debug.Log( $"{Agent._name}:Open:Enter()");
                _elapsed = 0;
            }

            protected override void OnUpdate()
            {
                _elapsed += Time.deltaTime;

                if(_elapsed > 1)
                {
                    Finish();
                }
            }

            protected override void OnExit()
            {

            }
        }

        private class Close : State<Door>
        {
            private float _elapsed;

            public Close(Door door) : base(door)
            {

            }
            protected override void OnEnter()
            {
                Debug.Log($"{Agent._name}:Close:Enter()");
                _elapsed = 0;
            }

            protected override void OnUpdate()
            {
                _elapsed += Time.deltaTime;

                if (_elapsed > 1)
                {
                    Finish();
                }
            }

            protected override void OnExit()
            {

            }
        }

        public StateMachine FSM;

        private string _name = "DOOR";

        public bool locked;

        public Door()
        {
            Open openState = new Open(this);
            Close closeState = new Close(this);
            openState.Transition = new Transition() { From = openState, To = closeState };
            closeState.Transition = new Transition() { From = closeState, To = openState };
            FSM = new StateMachine(openState);
        }
    }



    public class FSMExample : MonoBehaviour
    {

        Door door = new Door();
        private void Update()
        {
            door.FSM.Update();
        }
    }



    public class StateMachine
    {
        private State _initialState;
        private State _current;

        public StateMachine(State initialState)
        {
            _initialState = initialState;
            _current = initialState;
        }

        public void Update()
        {
            if (_current == null) return;
            switch (_current.Status)
            {
                case Status.Enter: _current.Enter(); break;
                case Status.Update: _current.Update(); break;
                case Status.Exit:
                    _current.Exit();
                    ChangeState();
                    break;
            }


        }

        private void ChangeState()
        {
            if (_current == null) return;
            if (_current.Transition == null)

            {
                _current = null;
                return;
            }
            if (_current.Transition.To == null) {
                _current = null;

                return; }
            _current = _current.Transition.To;
        }
    }

    public enum Status
    {
        Enter,
        Update,
        Exit
    }
    public abstract class State
    {
        public Transition Transition;

        public Status Status;

        private bool _finished;

        public void Enter()
        {
            _finished = false;
            Status = Status.Enter;
            OnEnter();
            Status = Status.Update;
        }
        
       protected virtual void OnEnter()
        {

        }

        public void Update()
        {
            Status = Status.Update;
            OnUpdate();

        }

        protected virtual void OnUpdate()
        {

        }

        public void Exit()
        {
            Status = Status.Exit;
            OnExit();

        }

        protected virtual void OnExit()
        {

        }

        protected void Finish()
        {
            Status = Status.Exit;
            _finished = true;
        }
    }

    public class Transition
    {
        public State From;
        public State To;

        public Condition Condition;

    }

    public abstract class Condition
    {
        public abstract bool Check();
    }


    public class StateMachine<TAgent> : StateMachine
    {
        protected TAgent Agent;
        public StateMachine(State init, TAgent agent) : base(init)
        {
            Agent = agent;
        }
    }

    public abstract class State<TAgent> : State
    {
        protected TAgent Agent;

        public State(TAgent agent)
        {
            Agent = agent;
        }

    }
}

