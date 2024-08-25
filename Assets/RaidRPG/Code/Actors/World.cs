using GameFramework.Actor;
using GameFramework.Actor.DataModel;
using GameFramework.Actor.View;
using GameFramework.Graph;
using RaidRPG.Actors.Actions;
using RaidRPG.Actors.States;
using RaidRPG.Actors.States.GameLogic;
using UnityEngine;

public class World : MonoBehaviour, IWorld
{
    [SerializeField] private ActorData _playerData;
    [SerializeField] private Gameplay _gameplay;
    public Camera Camera { get; private set;}
    public float DeltaTime { get; private set; }

    private Actor _player;

    private void Awake()
    {
        Camera = Camera.main;
        ActorFactory.Init();
        _player = ActorFactory.Create(_playerData, this);
        PlayerActiveState playerActiveState = new();
        Graph graph = new(playerActiveState);
        graph.SetAgent(_player);
        StateMachine<Actor> stateMachine = new(graph);
        playerActiveState.AddAction(ScriptableObject.CreateInstance<BindInputAction>());
        _player.SetLogicGraph(stateMachine);

        //What's the correct way to do this?
        _gameplay.PlayerTransform = _player.View.transform;
        _gameplay.Init();
    }

    private void Update()
    {
        DeltaTime = Time.deltaTime;
        _gameplay.Update(Time.deltaTime);
        _player.Update();
    }

}