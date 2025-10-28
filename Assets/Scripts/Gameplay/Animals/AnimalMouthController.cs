using System;
using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay.PlayerComponents;
using JetBrains.Annotations;
using R3;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Animals { 
    public class AnimalMouthController : MonoBehaviour {
        [Inject] private IPlayerProvider _playerProvider;
        [Inject] private GameplayConfig _gameplayConfig;

        private readonly CompositeDisposable _disposables = new();
        private readonly ReactiveProperty<bool> _isPlayerClose = new();

        private AnimalFoodReceiver _foodReceiver;
        [CanBeNull]
        private Transform _playerTransform;
        
        public ReadOnlyReactiveProperty<bool> IsPlayerClose => _isPlayerClose; 

        private void Start() {
            _playerProvider.Player.Subscribe(PlayerChanged).AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables?.Dispose();
        }

        private void Update() {
            if (_playerTransform == null) {
                SetIsPlayerClose(false);
                return;
            }
            
            var sqrDistanceToPlayer = Vector3.SqrMagnitude(transform.position - _playerTransform.position);
            var targetDistance = _gameplayConfig.AnimalInfo.AnimalDetectPlayerDistance;
            
            var shouldOpenMouth = sqrDistanceToPlayer < targetDistance * targetDistance;
            SetIsPlayerClose(shouldOpenMouth);
        }

        public void InjectComponents(AnimalFoodReceiver foodReceiver) {
            _foodReceiver = foodReceiver;
        }

        public void Initialize() {
            SetIsPlayerClose(false, force: true);
        }

        private void SetIsPlayerClose(bool isMouthOpened, bool force = false) {
            if (_isPlayerClose.CurrentValue == isMouthOpened && !force) {
                return;
            }

            _isPlayerClose.Value = isMouthOpened;
            _foodReceiver.SetActive(isMouthOpened);
        }

        private void PlayerChanged(Player player) {
            _playerTransform = player.transform;
        }
    }
}
