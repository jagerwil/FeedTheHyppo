using FeedTheHyppo.Architecture.SceneInitializers;
using FeedTheHyppo.Architecture.StateMachine.Gameplay;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay._Providers.Implementations;
using FeedTheHyppo.Gameplay._Services.Implementations;
using FeedTheHyppo.Gameplay.PlayerComponents;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture.Installers {
    public class GameplaySceneInstaller : MonoInstaller {
        [SerializeField] private Player _player;
        
        public override void InstallBindings() {
            BindServices();
            BindProviders();
            BindGameStates();
            BindInitializer();
        }

        private void BindServices() {
            Container.BindInterfacesTo<PlayerInputService>().AsSingle();
        }

        private void BindProviders() {
            Container.Bind<IPlayerProvider>()
                     .To<PlayerProvider>()
                     .AsSingle().WithArguments(_player);

            Container.Bind<IPlayerItemInteractionProvider>()
                     .To<PlayerItemInteractionProvider>()
                     .AsSingle();
        }

        private void BindGameStates() {
            Container.Bind<GameplayInitializationState>().AsSingle();
            Container.Bind<GameplayMainState>().AsSingle();
        }

        private void BindInitializer() {
            Container.BindInterfacesTo<GameplaySceneInitializer>().AsSingle();
        }
    }
}
