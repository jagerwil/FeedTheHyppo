using FeedTheHyppo.Configs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace FeedTheHyppo.Architecture.Installers {
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Configs/Configs Installer", order = 0)]
    public class ConfigsInstaller : ScriptableObjectInstaller {
        [SerializeField] private ScenesAddressesConfig _scenesAddressesConfig;
        [Space]
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings() {
            Container.Bind<ScenesAddressesConfig>().FromInstance(_scenesAddressesConfig).AsSingle();
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
        }
    }
}
