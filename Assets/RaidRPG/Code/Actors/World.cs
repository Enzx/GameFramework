using GameFramework.Actor;
using GameFramework.Actor.DataModel;
using GameFramework.Graph;
using RaidRPG.Actors.Actions;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private ActorData _playerData;

    private Actor _player;

    private void Awake()
    {
        ActorFactory.Init();
        _player = ActorFactory.Create(_playerData);
        ActiveState activeState = new();
        activeState.AddAction(new MoveAction());
        StateMachine<Actor> stateMachine = new(_player, activeState);
        _player.SetLogicGraph(stateMachine);
    }

    private void Update()
    {
        _player.Update(Time.deltaTime);
    }
}