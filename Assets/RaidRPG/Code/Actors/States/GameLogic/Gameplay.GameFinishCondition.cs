using GameFramework.Graph;

namespace RaidRPG.Actors.States.GameLogic
{
    public partial class Gameplay
    {
        public class GameFinishCondition : Condition<Gameplay>
        {
            protected override bool Check()
            {
                return Agent._enemies.Count == 0 && Agent._currentWave >= Agent.MaxWave;
            }
        }
    }
}