using GameFramework.Actor;
using System;
using UnityEngine;
using Behaviour = GameFramework.Actor.Behaviours.Behaviour;

namespace RaidRPG
{
    public class DashBehaviour : Behaviour, IDash
    {
 

        public DashBehaviour(Actor actor, DashData data) : base(actor, data)
        {
            actor.Events.Subscribe<InputMessage>(OnInputMessageReceived);
        }

        public void Dash(Vector2 direction)
        {
        }


        private void OnInputMessageReceived(InputMessage message)
        {
          if( message.Fire)
            {
                Debug.Log("Dash");
            }
        }
    }
}