using System;
using System.Collections.Generic;
using UnityEngine;


namespace Examples
{
    public interface IPublisher<TMessage>
    {
        void Publish(TMessage message);
    }

    public interface ISubscriber<TMessage>
    {
        void Subscribe(Action<TMessage> message);

    }

    public class Broker<TMessage> : IPublisher<TMessage>, ISubscriber<TMessage>
    {
        private List<Action<TMessage>> handlers = new List<Action<TMessage>>();
        public Filter<TMessage> filter;
        public void Publish(TMessage message)
        {
            if(filter != null)
            foreach (var handler in handlers)
            {
                if (filter.Apply(message))
                    handler.Invoke(message);
            }

            if (filter == null)
                foreach (var handler in handlers)
                {
                    handler.Invoke(message);
                }
        }

        public void Subscribe(Action<TMessage> handler)
        {
            handlers.Add(handler);
        }


    }

    public abstract class Filter<TMessage>
    {
        public abstract bool Apply(TMessage message);
    }

    public class DuplicationFilter : Filter<InputMessage>
    {
        int preValue = 0;
        public override bool Apply(InputMessage message)
        {
            bool isDuplicated = preValue != message.counter;
            preValue = message.counter;

            return isDuplicated;
        }
    }



    public class EventExamples : MonoBehaviour
    {
        Broker<InputMessage> broker = new Broker<InputMessage>();

        InputSystem input;
        Movement movement;
        Weapon weapon = new Weapon();



        private void Awake()
        {
            input = new InputSystem(broker);
            broker.filter = new DuplicationFilter();
            movement = new Movement(broker);
        }

        private void Update()
        {
            input.Update();
        }

    }





    public class InputMessage
    {
        public bool MovePressed;

        public int counter;
    }

    public class InputSystem
    {
        private IPublisher<InputMessage> _publisher;
        public InputSystem(IPublisher<InputMessage> publisher)
        {
            _publisher = publisher;
        }
        int counter = 0;
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                counter++;


                InputMessage message = new InputMessage { MovePressed = true, counter = this.counter };
                _publisher.Publish(message);
                counter++;

                _publisher.Publish(message);
                counter++;

                _publisher.Publish(message);
                counter++;

                _publisher.Publish(message);
            }

        }
    }

    public class Movement
    {
        public Movement(ISubscriber<InputMessage> subscriber)
        {
            subscriber.Subscribe(InputCallback);
        }

        void InputCallback(InputMessage message)
        {
            Debug.Log(message.counter);
            if (message.MovePressed) Move();
        }


        public void Move()
        {
            Debug.Log("Move!");
        }
    }

    public class Weapon
    {
        public void Shoot()
        {
            Debug.Log("Shoot!");

        }
    }
}


