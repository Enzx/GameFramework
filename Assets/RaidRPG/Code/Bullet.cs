using System;
using UnityEngine;

namespace RaidRPG
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 2f;
        [SerializeField] private float _damage = 3f;

        private Rigidbody2D _rigidbody;
        private Vector2 _direction;
        private float _startTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _startTime = Time.time;
        }


        private void Update()
        {
            //Move bullet based on direction and speed applying on rigidbody2d
            Move();
            //Destroy bullet after _lifeTime seconds
            if (Time.time - _startTime > _lifeTime)
            {
                Kill();
            }
        }

        //Kill the bullet
        private void Kill()
        {
            Destroy(gameObject);
        }

        //Move bullet based on direction and speed applying on rigidbody2d
        private void Move()
        {
            //Apply movement to rigidbody2d
            _rigidbody.velocity = _direction * _speed;
        }

        //Set the direction of the bullet
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Destroy bullet when it hits an enemy
            if (other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
                //Damage enemy
                other.GetComponent<Enemy>().Damage(_damage);
            }
        }
    }
}