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
        //[SerializeField] private GameObject _mouthRoot;
        
        [Inject] private IPlayerProvider _playerProvider;
        [Inject] private GameplayConfig _gameplayConfig;

        private readonly CompositeDisposable _disposables = new();

        private AnimalFoodReceiver _foodReceiver;
        [CanBeNull]
        private Transform _playerTransform;
        private bool _isMouthOpened = false;

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
            var targetDistance = _gameplayConfig.AnimalDetectPlayerDistance;
            
            var shouldOpenMouth = sqrDistanceToPlayer < targetDistance * targetDistance;
            SetIsMouthOpened(shouldOpenMouth);
        }

        public void InjectComponents(AnimalFoodReceiver foodReceiver) {
            _foodReceiver = foodReceiver;
        }

        public void Initialize() {
            SetIsMouthOpened(false, force: true);
        }

        private void SetIsMouthOpened(bool isMouthOpened, bool force = false) {
            if (_isMouthOpened == isMouthOpened && !force) {
                return;
            }

            _isMouthOpened = isMouthOpened;

            //_mouthRoot.SetActive(_isMouthOpened);
            _foodReceiver.SetActive(_isMouthOpened);
        }

        private void PlayerChanged(Player player) {
            _playerTransform = player.transform;
        }
    }
}
