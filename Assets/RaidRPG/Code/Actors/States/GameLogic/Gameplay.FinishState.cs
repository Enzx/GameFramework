using GameFramework.Graph;
using UnityEngine;

namespace RaidRPG.Actors.States.GameLogic
{
    public partial class Gameplay
    {
        public class FinishState : State<Gameplay>
        {
            protected override void OnEnter()
            {
                Debug.Log("Game Finished!");
            }

            protected override void OnUpdate(float deltaTime)
            {
            }

            protected override void OnExit()
            {
            }
            
        }
    }
}