using GameFramework.Graph;
using UnityEngine;

namespace RaidRPG.Actors.States.GameLogic
{
    public partial class Gameplay
    {
        public class InitState : State<Gameplay>
        {
            protected override void OnEnter()
            {
                Agent._currentWave = 0;
                Debug.Log("Game Started!");
            }

            protected override void OnUpdate(float deltaTime)
            {
                Finish(true);
            }

            protected override void OnExit()
            {
            }

            public InitState(StateData data) : base(data)
            {
            }
        }
    }
}