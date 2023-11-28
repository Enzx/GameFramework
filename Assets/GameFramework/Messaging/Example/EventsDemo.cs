using UnityEngine;

namespace GameFramework.Messaging.Example
{
    public class EventsDemo : MonoBehaviour
    {
        private readonly Events _events = new Events();

        private void Awake()
        {
            _events.Subscribe<TextMessage>(TextMessageHandler);
            _events.Subscribe<ActivationMessage>(ActivationHandler);
            _events.Subscribe<int>(i => { print(i); });

            Message message = new TextMessage() { Text = "Text Message" };
            if (_events.Publish(message) == false)
            {
                print($"There is no broker for {message.GetType()}");
            }

            message = new ActivationMessage();
            if (_events.Publish(message) == false)
            {
                print($"There is no broker for {message.GetType()}");
            }
            
            _events.Publish(0);
        }
        
        private void TextMessageHandler(TextMessage msg)
        {
            print(msg);
        }

        private void ActivationHandler(ActivationMessage msg)
        {
            print(msg);
        }
    }
}