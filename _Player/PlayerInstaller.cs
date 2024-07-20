using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Zenject.SpaceFighter;

namespace Assets.KVLC
{
    public class PlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                Debug.LogError("Player not found.");
                return;
            }

            ComponentBinding(player);
            PlayerBinding();
            PlayerConstructorBinding();
        }

        private void ComponentBinding(GameObject player)
        {
            Container.Bind<Animator>().FromComponentOn(player).AsSingle();
            Container.Bind<CharacterController>().FromComponentOn(player).AsSingle();
            Container.Bind<InputHandler>().FromComponentOn(player).AsSingle();
            Container.Bind<PlayerInput>().FromComponentOn(player).AsSingle();
            Container.Bind<FirstPersonController>().FromComponentOn(player).AsSingle();
        }

        private void PlayerBinding()
        {
            Container.Bind<PlayerData>().AsSingle();
            Container.Bind<SaveManager>().AsSingle();
            Container.Bind<SaveLoadService>().AsSingle();
        }

        private void PlayerConstructorBinding()
        {
            Container.Bind<Player>().AsSingle().OnInstantiated<Player>((context, playerInstance) =>
            {
                var inputHandler = playerInstance.Input;
                var saveLoadService = playerInstance.SaveLoadService;

                inputHandler.PlayerInput = Container.Resolve<PlayerInput>();
                saveLoadService = Container.Resolve<SaveLoadService>();

                playerInstance.LoadPlayerData();
            });
        }
    }
}
