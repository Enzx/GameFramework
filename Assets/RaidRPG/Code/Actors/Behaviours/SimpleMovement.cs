using System;
using System.Collections.Generic;
using GameFramework.Actor;
using UnityEngine;
using Behaviour = GameFramework.Actor.Behaviours.Behaviour;

namespace RaidRPG
{
   
    
    public class SimpleMovement : Behaviour, IMovement
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly SimpleMovementData _data;
        private Vector2 _direction;

        public SimpleMovement(Actor actor, SimpleMovementData data) : base(actor, data)
        {
            _data = data;
            _rigidbody = actor.View.GetComponent<Rigidbody2D>();
          //  actor.Events.Subscribe<InputMessage>(OnInputMessageReceived);
        }

        private void OnInputMessageReceived(InputMessage message)
        {
            Move(message.MoveDirection);
        }

        public void Move(Vector2 direction)
        {
            _direction = direction;
            _rigidbody.velocity = direction * _data.Speed.Value;
            _direction = Vector2.zero;

        }

        public override void Update(float deltaTime)
        {
            _direction = Vector2.zero;
        }
    }
}