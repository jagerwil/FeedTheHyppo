using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay.Items;
using Jagerwil.Core.Architecture.Factories.Implementations;
using Jagerwil.Core.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace FeedTheHyppo.Gameplay._Factories.Implementations {
    public class FoodItemFactory : BaseGameFactory<FoodItem>, IFoodItemFactory {
        private readonly PrefabsAddressesConfig _prefabsAddressesConfig;
        
        public FoodItemFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            FactoryPoolsConfig poolsConfig,
            PrefabsAddressesConfig addressesConfig,
            Transform defaultRoot)
            : base(instantiator, addressablesLoader, poolsConfig.FoodItemFactoryPool, defaultRoot) {
            _prefabsAddressesConfig = addressesConfig;

            BaseItem.onDespawnRequested += TryDespawn;
        }
        
        public FoodItem Spawn(Transform spawnPoint) {
            var foodItem = CreateInternal(spawnPoint.position, spawnPoint.rotation, spawnPoint);
            if (!foodItem) {
                return null;
            }
            
            foodItem.SetDefaultRoot(_defaultRoot);
            return foodItem;
        }
        
        protected override AssetReferenceGameObject GetAssetReference() {
            return _prefabsAddressesConfig.Melon;
        }

        private void TryDespawn(BaseItem baseItem) {
            var foodItem = baseItem as FoodItem;
            if (foodItem) {
                Despawn(foodItem);
            }
        }
    }
}
