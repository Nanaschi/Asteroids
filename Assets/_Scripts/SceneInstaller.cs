using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    
    public override void InstallBindings()
    {
        var playerInputActions = new PlayerInputActions();
        Container.Bind<PlayerInputActions>().FromInstance(playerInputActions).AsSingle().NonLazy();
        playerInputActions.Enable();
        Container.BindFactory<Player, Player.Factory>().FromComponentInNewPrefab(_playerPrefab);
    }
}
