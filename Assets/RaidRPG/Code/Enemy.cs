using System;
using UnityEngine;

namespace RaidRPG
{
    public class Enemy : MonoBehaviour
    {
        
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _maxHealth = 10f;
        
        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private Transform _target;
        private float _health;
        
        public bool IsDead => _health > 0;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _health = _maxHealth;
        }

        private void Update()
        {
            //Update direction based on target position
            UpdateDirection();
            //Move enemy based on direction and speed applying on rigidbody2d
            Move();
            //Rotate enemy based on direction
            Rotate();
        }

        //Set the direction of the enemy
        public void SetSpeed(float randomSpeed)
        {
            _speed = randomSpeed;
        }

        //Set the direction of the enemy
        public void SetTarget(Transform target)
        {
            _target = target;
        }

        //Update direction based on target position
        private void UpdateDirection()
        {
            //Get direction from enemy to target
            Vector3 position = transform.position;
            Vector2 direction = _target.position - position;
            direction.Normalize(); // Normalize the direction
            _direction = direction;
        }
        
        //Rotate enemy based on direction, the enemy will look at the direction it is moving and Vectro3.up is the default direction
        private void Rotate()
        {
            //Get angle from direction
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            //Rotate enemy based on angle
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

   

        //Move enemy based on direction and speed applying on rigidbody2d
        private void Move()
        {
            //Apply movement to rigidbody2d
            _rigidbody.velocity = _direction * _speed;
        }

        public void Damage(float damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Kill();
            }
        }

        private void Kill()
        {
            Destroy(gameObject);
            
        }
    }
}