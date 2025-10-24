using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay.PlayerComponents;
using Jagerwil.Core.Architecture.Factories.Implementations;
using Jagerwil.Core.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace FeedTheHyppo.Gameplay._Factories.Implementations {
    public class PlayerFactory : BaseGameFactory<Player>, IPlayerFactory {
        private readonly PrefabsAddressesConfig _prefabsAddressesConfig;

        public PlayerFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            PrefabsAddressesConfig prefabsAddressesConfig,
            FactoryPoolsConfig factoryPoolsConfig,
            Transform defaultRoot)
            : base(instantiator, addressablesLoader, factoryPoolsConfig.PlayerFactoryPool, defaultRoot) {
            _prefabsAddressesConfig = prefabsAddressesConfig;
        }
        
        protected override AssetReferenceGameObject GetAssetReference() {
            return _prefabsAddressesConfig.Player;
        }
        
        public Player Spawn() {
            var foodItem = CreateInternal();
            return foodItem;
        }
    }
}
