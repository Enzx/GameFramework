using UnityEngine;

namespace GameFramework.Actor.View
{
    public interface IWorld
    {
        Camera Camera { get; }
        float DeltaTime { get; }
    }
}