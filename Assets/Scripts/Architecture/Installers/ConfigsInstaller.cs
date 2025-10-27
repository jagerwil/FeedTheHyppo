using FeedTheHyppo.Configs;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture.Installers {
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Configs/Configs Installer", order = 0)]
    public class ConfigsInstaller : ScriptableObjectInstaller {
        [SerializeField] private ScenesAddressesConfig _scenesAddressesConfig;
        [SerializeField] private PrefabAddressesConfig _prefabsAddressesConfig;
        [SerializeField] private FactoryPoolsConfig _factoryPoolsConfig;
        [Space]
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GameplayConfig _gameplayConfig;
        [SerializeField] private AudioConfig _audioConfig;
        
        public override void InstallBindings() {
            Container.Bind<ScenesAddressesConfig>().FromInstance(_scenesAddressesConfig).AsSingle();
            Container.Bind<PrefabAddressesConfig>().FromInstance(_prefabsAddressesConfig).AsSingle();
            Container.Bind<FactoryPoolsConfig>().FromInstance(_factoryPoolsConfig).AsSingle();
            
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
            Container.Bind<GameplayConfig>().FromInstance(_gameplayConfig).AsSingle();
            Container.Bind<AudioConfig>().FromInstance(_audioConfig).AsSingle();
        }
    }
}
