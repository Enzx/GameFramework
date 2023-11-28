using GameFramework.Actor;
using GameFramework.Actor.DataModel;
using RaidRPG.Actors.DataModel;
using UnityEngine;
using Behaviour = GameFramework.Actor.Behaviours.Behaviour;

namespace RaidRPG.Actors.Behaviours
{
    public class WeaponBehaviour : Behaviour, IWeapon
    {
        private Bullet _bulletPrefab;

        public WeaponBehaviour(Actor actor, AttackData data) : base(actor, data)
        {
            _bulletPrefab = data.BulletPrefab;
        }

        public void Shoot()
        {
            Vector3 weaponPosition = Actor.View.WeaponTransform.position;
            Vector2 direction = weaponPosition - Actor.View.transform.position;
            direction.Normalize();

            Bullet bullet = Object.Instantiate(_bulletPrefab, weaponPosition, Quaternion.identity);
            bullet.SetDirection(direction);
        }

        public void SetDirection(Vector3 targetPosition)
        {
            Vector3 position = Actor.View.transform.position;
            Vector2 direction = targetPosition - new Vector3(position.x, position.y);
            direction.Normalize();

            // Set the distance from the player (radius of the circle)
            float distanceFromPlayer = 1.0f; // Change this value as needed

            // Calculate the weapon's position
            Vector2 weaponPosition = (Vector2)position + direction * distanceFromPlayer;

            // Update the weapon's position
            Actor.View.WeaponTransform.position = weaponPosition;
        }
    }
}