using UnityEngine;

namespace RaidRPG
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _weaponTransform;
        [SerializeField] private Transform _bulletPrefab;
        [SerializeField] private float _fireRate;
        [SerializeField] private bool _canSwim;
        [SerializeField] private float _swimSpeed;

        private Rigidbody2D _rigidbody;
        private Camera _camera;
        private float _lastShotTime;
        private bool _isSwimming;

        private void Awake()
        {
            _camera = Camera.main;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //Get input direction (AWSD or arrows)
            Vector2 direction = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            //Move player based on input direction
            //Check if we hit the water trigger on next frame stop player movement otherwise check if player can swim
            //Get player sprite radius for hit distance
            float playerRadius = GetComponent<SpriteRenderer>().bounds.extents.x;
            //Get player position
            Vector2 playerPosition = transform.position;
            //Get player direction
            Vector2 playerDirection = direction.normalized;
            //Get player hit distance
            float playerHitDistance = playerRadius + 0.1f;
            //Get player hit position
            Vector2 playerHitPosition = playerPosition + playerDirection * playerHitDistance;
            //Get player hit layer
            LayerMask playerHitLayer = LayerMask.GetMask("Water");
            //Check if player hit water
            bool playerHitWater = Physics2D.OverlapCircle(playerHitPosition, playerRadius, playerHitLayer);
            //Check if player hit water
            if (playerHitWater)
            {
                //Check if player can swim
                if (!_canSwim)
                {
                    //Stop player movement
                    direction = Vector2.zero;
                }
                else
                {
                    _isSwimming = true;
                }
            }
            else
            {
                _isSwimming = false;
            }
            
            //Convert player position to screen space and check player direction will move player out of screen
            Vector2 playerScreenPosition = _camera.WorldToScreenPoint(playerPosition);
            //Check if player direction will move player out of screen
            if (playerScreenPosition.x < 0 && direction.x < 0)
            {
                direction.x = 0;
            }
            else if (playerScreenPosition.x > Screen.width && direction.x > 0)
            {
                direction.x = 0;
            }
            else if (playerScreenPosition.y < 0 && direction.y < 0)
            {
                direction.y = 0;
            }
            else if (playerScreenPosition.y > Screen.height && direction.y > 0)
            {
                direction.y = 0;
            }

            Move(direction);
            RotateWeapon();
            CheckFire();
        }

        //Move player based on input direction and speed applying on rigidbody2d
        private void Move(Vector2 direction)
        {
            //Check if player is swimming
            float speed = _isSwimming ? _swimSpeed : _speed;
            //Get input direction and apply speed to it
            Vector2 movement = direction * speed;
            //Apply movement to rigidbody2d
            _rigidbody.velocity = movement;
        }

        //set the weapon position based on mouse coord. on world space in a circle around player
        private void RotateWeapon()
        {
            // Convert mouse position to world space
            Vector2 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            // Get direction from player to mouse
            Vector3 position = transform.position;
            Vector2 direction = mouseWorldPosition - new Vector2(position.x, position.y);
            direction.Normalize(); // Normalize the direction

            // Set the distance from the player (radius of the circle)
            float distanceFromPlayer = 1.0f; // Change this value as needed

            // Calculate the weapon's position
            Vector2 weaponPosition = (Vector2)position + direction * distanceFromPlayer;

            // Update the weapon's position
            _weaponTransform.position = weaponPosition;
        }

        //Shoot a bullet on the direction of the weapon (mouse position)
        public void Shoot()
        {
            // Get the direction from the player to the mouse
            Vector2 direction = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize(); // Normalize the direction

            // Create the bullet
            Transform bulletTransform = Instantiate(_bulletPrefab, _weaponTransform.position, Quaternion.identity);
            Bullet bullet = bulletTransform.GetComponent<Bullet>();
            bullet.SetDirection(direction);
        }

        //Check if fire button is pressed and the time since last shoot is greater than fire rate and if so, shoot a bullet
        private void CheckFire()
        {
            // Check if the fire button is pressed
            if (Input.GetButton("Fire1"))
                // Check if the time since the last shot is greater than the fire rate
                if (Time.time - _lastShotTime > _fireRate)
                {
                    // Shoot a bullet
                    Shoot();

                    // Update the last shot time
                    _lastShotTime = Time.time;
                }
        }
    }
}