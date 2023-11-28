using GameFramework.Actor.DataModel;

namespace RaidRPG
{
    public class Health : Attrib<float>
    {
        public Health(float initialValue, float minValue, float maxValue) : base(initialValue, minValue, maxValue)
        {
        }
    }
}