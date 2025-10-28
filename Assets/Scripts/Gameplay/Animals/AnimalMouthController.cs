using System;
using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay.Items;
using FeedTheHyppo.Gameplay.PlayerComponents;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Animals { 
    public class AnimalMouthController : MonoBehaviour {
        [Inject] private IPlayerProvider _playerProvider;
        [Inject] private IPlayerItemInteractionProvider _playerItemProvider;
        [Inject] private GameplayConfig _gameplayConfig;

        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<bool> _isMouthOpened = new();

        private AnimalFoodReceiver _foodReceiver;
        [CanBeNull]
        private Transform _playerTransform;
        
        public ReadOnlyReactiveProperty<bool> IsMouthOpened => _isMouthOpened; 

        private void Start() {
            _playerProvider.Player.Subscribe(PlayerChanged).AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables?.Dispose();
        }

        private void Update() {
            if (_playerTransform == null) {
                SetIsMouthOpened(false);
                return;
            }
            
            var sqrDistanceToPlayer = Vector3.SqrMagnitude(transform.position - _playerTransform.position);
            var targetDistance = _gameplayConfig.AnimalInfo.AnimalDetectPlayerDistance;
            
            var isCloseToPlayer = sqrDistanceToPlayer < targetDistance * targetDistance;
            
            var equippedItem = _playerItemProvider.EquippedItem.CurrentValue;
            var hasFood = equippedItem != null && equippedItem is FoodItem;
            SetIsMouthOpened(isCloseToPlayer && hasFood);
        }

        public void InjectComponents(AnimalFoodReceiver foodReceiver) {
            _foodReceiver = foodReceiver;
        }

        public void Initialize() {
            SetIsMouthOpened(false, force: true);
        }

        private void SetIsMouthOpened(bool isMouthOpened, bool force = false) {
            if (_isMouthOpened.CurrentValue == isMouthOpened && !force) {
                return;
            }

            _isMouthOpened.Value = isMouthOpened;
            _foodReceiver.SetActive(isMouthOpened);
        }

        private void PlayerChanged(Player player) {
            _playerTransform = player.transform;
        }
    }
}
