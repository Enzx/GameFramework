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
        private readonly List<Enemy> _enemies;
        [NonSerialized] public Transform PlayerTransform;

        private StateMachine<Gameplay> _stateMachine;

        public Gameplay()
        {
            _enemies = new List<Enemy>();
            InitState initState = new();
            SpawnState spawnState = new();
            GameFinishCondition finishCondition = new();
            FinishState finishState = new();

            _stateMachine = new StateMachine<Gameplay>(this, initState);
            _stateMachine.AddState(spawnState);
            _stateMachine.AddState(finishState);
            _stateMachine.AddCondition(finishCondition);

            _stateMachine.AddTransition(initState, spawnState);
            _stateMachine.AddTransition(spawnState, finishCondition);
            _stateMachine.AddTransition(finishCondition, finishState);
        }

        public void Update(float deltaTime)
        {
            _stateMachine.DeltaTime = deltaTime;
            _stateMachine.Execute();
        }
    }
}