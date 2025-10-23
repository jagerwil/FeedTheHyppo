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
            BaseItem.onItemTaken -= ItemTaken;
        }
        #endregion

        #region Public Methods
        public void Initialize() {
            _spawnPoints.Initialize(_gameplayConfig.MelonSpawnInterval, SpawnFood);

            BaseItem.onItemTaken -= ItemTaken;
            BaseItem.onItemTaken += ItemTaken;
        }
        #endregion

        #region Private Methods
        private void ItemTaken(BaseItem item) {
            _spawnPoints.TakeObject(item.gameObject);
        }

        private GameObject SpawnFood(SpawnPoint spawnPoint) {
            return _foodItemsFactory.Spawn(spawnPoint.transform)?.gameObject;
        }
        #endregion
    }
}
