using _Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Scripts
{
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
            Container.Bind<Player>().FromInstance(_playerPrefab).AsSingle().NonLazy();

            Container.Bind<GameUIView>().FromInstance(_gameUIView).NonLazy();
            Container.Bind<GameUIController>().AsSingle().NonLazy();
        }
    }
}