using FeedTheHyppo.Architecture.SceneInitializers;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture.Installers {
    public class EntryPointSceneInstaller : MonoInstaller {
        public override void InstallBindings() {
            Container.BindInterfacesTo<EntryPointSceneInitializer>().AsSingle();
        }
    }
}
