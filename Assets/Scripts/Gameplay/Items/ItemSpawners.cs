using System;
using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay._Factories;
using Jagerwil.Core.Utils.Spawning;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Items {
    public class ItemSpawners : MonoBehaviour {
        #region Serialized & Injected Fields
        [SerializeField] private SeparateSpawnPoints _spawnPoints;

        [Inject] private IFoodItemFactory _foodItemsFactory;
        [Inject] private GameplayConfig _gameplayConfig;
        #endregion

        #region Unity Callbacks
        private void OnDestroy() {
            BaseItem.onItemStateChanged -= ItemStateChanged;
        }
        #endregion

        #region Public Methods
        public void Initialize() {
            _spawnPoints.Initialize(_gameplayConfig.MelonSpawnInterval, SpawnFood);

            BaseItem.onItemStateChanged -= ItemStateChanged;
            BaseItem.onItemStateChanged += ItemStateChanged;
        }
        #endregion

        #region Private Methods
        private void ItemStateChanged(BaseItem item, ItemState state) {
            if (state == ItemState.InPlace) {
                _spawnPoints.TakeObject(item.gameObject);
            }
        }

        private GameObject SpawnFood(SpawnPoint spawnPoint) {
            return _foodItemsFactory.Spawn(spawnPoint.transform)?.gameObject;
        }
        #endregion
    }
}
