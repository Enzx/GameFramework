using System;
using System.Collections.Generic;
using GameFramework.Graph;
using UnityEngine;

namespace RaidRPG.Actors.States.GameLogic
{
    [Serializable]
    public partial class Gameplay
    {
        public Enemy EnemyPrefab;
        public float WaveDistance;
        public int MaxWave;
        private int _currentWave;
        private  List<Enemy> _enemies;
        [NonSerialized] public Transform PlayerTransform;

        private StateMachine<Gameplay> _stateMachine;

        public void  Init()
        {
            _enemies = new List<Enemy>();
            InitState initState = new();
            SpawnState spawnState = new();
            GameFinishCondition finishCondition = new();
            FinishState finishState = new();

            Graph graph = new(initState);
            graph
                .ConnectTo(spawnState)
                .ConnectTo(finishCondition)
                .ConnectTo(finishState)
                .SetAgent(this);

            _stateMachine = new StateMachine<Gameplay>(graph);
        }

        public void Update(float deltaTime)
        {
            _stateMachine.DeltaTime = deltaTime;
            _stateMachine.Execute();
        }
    }
}