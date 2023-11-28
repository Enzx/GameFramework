using System.Collections.Generic;
using UnityEngine;

namespace RaidRPG
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _maxWave = 3;
        [SerializeField] private float _waveDistance = 5f;

        private Player _player;
        private readonly List<Enemy> _enemies = new();
        private readonly Dictionary<int, int> _waves = new();
        private int _currentWave = 0;
        private bool _finished;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
            PopulateWave();
            _currentWave = 0;
            _finished = false;
        }

        private void Update()
        {
            if (_finished) return;

            //Iterate on enemy list and remove dead enemies, if list is empty spawn next wave
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                //Check if enemy is dead
                if (!_enemies[i].IsDead)
                {
                    //Remove enemy from list
                    _enemies.RemoveAt(i);
                }
            }

            //Check if enemy list is empty
            if (_enemies.Count == 0)
            {
                //Check if current wave index is less than max wave
                if (_currentWave < _maxWave)
                {
                    //Spawn next wave
                    SpawnWave();
                }
                else
                {
                    print("You win!");
                    _finished = true;
                    return;
                }

                //Increment current wave index
                _currentWave++;
            }
        }

        //Spawn enemy at random position on a circle around player with random speed with a min distance from player
        public void SpawnEnemy()
        {
            //Get random position on a circle around player
            Vector2 randomPosition = RandomCircle(_player.transform.position, _waveDistance);
            //Get random speed
            float randomSpeed = Random.Range(1.0f, 5.0f);
            //Spawn enemy
            Enemy enemy = Instantiate(_enemyPrefab, randomPosition, Quaternion.identity);
            //Set enemy speed
            enemy.SetSpeed(randomSpeed);
            //Set enemy target
            enemy.SetTarget(_player.transform);
            //Add enemy to list
            _enemies.Add(enemy);
        }

        //Populate wave dictionary with enemy count  = (2 x wave index + 1)
        private void PopulateWave()
        {
            for (int i = 0; i < _maxWave; i++)
            {
                _waves.Add(i, 2 * i + 1);
            }
        }

        //Spawn wave of enemies based on current wave index
        private void SpawnWave()
        {
            //Get enemy count from wave dictionary
            int enemyCount = _waves[_currentWave];
            //Spawn enemyCount enemies
            for (int i = 0; i < enemyCount; i++)
            {
                SpawnEnemy();
            }
        }


        //Get random position on a circle around center with a distance
        private static Vector2 RandomCircle(Vector3 center, float distance)
        {
            // create random angle between 0 to 360 degrees 
            float ang = Random.value * 360;
            Vector2 pos;
            pos.x = center.x + distance * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = center.y + distance * Mathf.Cos(ang * Mathf.Deg2Rad);
            return pos;
        }
    }
}