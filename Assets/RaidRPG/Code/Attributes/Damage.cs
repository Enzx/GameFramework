using GameFramework.Actor.DataModel;

namespace RaidRPG
{
    public class Damage : Attrib<float>
    {
        public Damage(float initialValue, float minValue, float maxValue) : base(initialValue, minValue, maxValue)
        {
        }
    }
}