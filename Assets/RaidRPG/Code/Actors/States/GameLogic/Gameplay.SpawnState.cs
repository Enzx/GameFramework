using System.Collections.Generic;
using GameFramework.Graph;
using UnityEngine;

namespace RaidRPG.Actors.States.GameLogic
{
    public partial class Gameplay
    {
        public class SpawnState : State<Gameplay>
        {
            private readonly Dictionary<int, int> _waves = new();

            protected override void OnEnter()
            {
                PopulateWaves();
                Agent._currentWave = 0;
            }

            protected override void OnUpdate(float deltaTime)
            {
                //TODO: Add an Event for enemy death
                for (int i = Agent._enemies.Count - 1; i >= 0; i--)
                {
                    if (!Agent._enemies[i].IsDead)
                    {
                        Agent._enemies.RemoveAt(i);
                    }
                }

                //TODO: Add an Event for wave spawn
                if (Agent._enemies.Count == 0)
                {
                    if (Agent._currentWave < Agent.MaxWave)
                    {
                        SpawnWave();
                    }
                    else
                    {
                        Finish(true);
                        return;
                    }

                    Agent._currentWave++;
                }
            }

            protected override void OnExit()
            {
                Agent._enemies.Clear();
            }


            public void SpawnEnemy()
            {
                Vector2 randomPosition = RandomCircle(Agent.PlayerTransform.position, Agent.WaveDistance);
                float randomSpeed = Random.Range(1.0f, 5.0f);
                Enemy enemy = Object.Instantiate(Agent.EnemyPrefab, randomPosition, Quaternion.identity);
                enemy.SetSpeed(randomSpeed);
                enemy.SetTarget(Agent.PlayerTransform);
                Agent._enemies.Add(enemy);
            }

            private void PopulateWaves()
            {
                for (int i = 0; i < Agent.MaxWave; i++)
                {
                    _waves.Add(i, 2 * i + 1);
                }
            }

            //Spawn wave of enemies based on current wave index
            private void SpawnWave()
            {
                //Get enemy count from wave dictionary
                int enemyCount = _waves[Agent._currentWave];
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
}