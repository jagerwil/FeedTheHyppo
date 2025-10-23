using FeedTheHyppo.Architecture.SceneInitializers;
using FeedTheHyppo.Architecture.StateMachine.Gameplay;
using FeedTheHyppo.Gameplay._Services;
using FeedTheHyppo.Gameplay._Services.Implementations;
using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture.Installers {
    public class GameplaySceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            BindServices();
            BindGameStates();
            BindInitializer();
        }

        private void BindServices() {
            Container.BindInterfacesTo<PlayerInputService>().AsSingle();
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
