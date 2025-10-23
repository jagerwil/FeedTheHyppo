using FeedTheHyppo.Architecture.Services;
using FeedTheHyppo.Architecture.Services.Implementations;
using FeedTheHyppo.Architecture.StateMachine;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using Jagerwil.Core.Services.Implementations;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture.Installers {
    public class ProjectInstaller : MonoInstaller {
        public override void InstallBindings() {
            BindServices();
            BindGameStateMachine();
        }

        private void BindServices() {
            Container.Bind<IAddressablesLoader>().To<AddressablesLoader>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
        }

        private void BindGameStateMachine() {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<InitializationState>().AsSingle();
            Container.Bind<SceneLoadingState>().AsSingle();
        }
    }
}
