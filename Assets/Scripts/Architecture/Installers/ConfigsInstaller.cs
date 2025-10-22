using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture.DependencyInstallers {
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Configs/Configs Installer", order = 0)]
    public class ConfigsInstaller : ScriptableObjectInstaller {
        [SerializeField] private ScenesAddressesConfig _scenesAddressesConfig;
        
        public override void InstallBindings() {
            Container.Bind<ScenesAddressesConfig>().FromInstance(_scenesAddressesConfig).AsSingle();
        }
    }
}
