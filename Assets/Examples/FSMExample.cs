using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Examples
{
    public class Door
    {
        private class Open : State
        {
            private float _elapsed;
            protected override void OnEnter()
            {
                Debug.Log("Door is Open");
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
                Debug.Log("Door is Open:Exit()");

            }
        }

        private class Close : State
        {
            private float _elapsed;
            protected override void OnEnter()
            {
                Debug.Log("Door:Close:Enter()");
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
                Debug.Log("Door is Close:Exit()");

            }
        }

        public StateMachine FSM;
        
        public Door()
        {
            Open openState = new Open();
            Close closeState = new Close();
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
}

