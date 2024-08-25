using System;
using GameFramework.Actor.DataModel;

namespace RaidRPG
{
    [Serializable]
    public class Speed : Attrib<float>
    {
        public Speed(float initialValue, float minValue, float maxValue) : base(initialValue, minValue, maxValue)
        {
        }
    }
}