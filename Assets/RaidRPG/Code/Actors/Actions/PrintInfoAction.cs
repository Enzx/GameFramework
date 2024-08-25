using System;
using GameFramework.Actor;
using GameFramework.Graph;
using UnityEngine;

namespace RaidRPG.Actors.Actions
{
    [Serializable]
    public class PrintInfoAction : ActionTask<Actor>
    {
        public string Message;

        public override Result Execute()
        {
            Debug.Log($"{Agent}: {Message}");
            return Result.Success;
        }
    }
}