using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private GameUIView _gameUIView;

    public override void InstallBindings()
    {
        var playerInputActions = new PlayerInputActions();
        Container.Bind<PlayerInputActions>().FromInstance(playerInputActions).AsSingle().NonLazy();

        playerInputActions.Enable();
        Container.BindFactory<Player, Player.Factory>().FromComponentInNewPrefab(_playerPrefab);

        Container.Bind<GameUIView>().FromInstance(_gameUIView).NonLazy();
        Container.Bind<GameUIController>().AsSingle().NonLazy();
    }
}