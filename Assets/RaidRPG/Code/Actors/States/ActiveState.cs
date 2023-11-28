using System.Collections;
using System.Collections.Generic;
using GameFramework.Actor;
using GameFramework.Graph;
using RaidRPG;
using UnityEngine;

public class ActiveState : State<Actor>
{
    private InputBehaviour _input;

    protected override void OnEnter()
    {
        _input = Agent.Behaviors.Get<InputBehaviour>();
    }


    protected override void OnUpdate(float deltaTime)
    {
        _input.Update(deltaTime);
    }

    protected override void OnExit()
    {

    }
}
