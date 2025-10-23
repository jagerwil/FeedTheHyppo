using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay._Providers;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Animals {
    public class AnimalMouthController : MonoBehaviour {
        [SerializeField] private GameObject _mouthRoot;
        
        [Inject] private IPlayerProvider _playerProvider;
        [Inject] private GameplayConfig _gameplayConfig;

        private AnimalFoodReceiver _foodReceiver;
        private Transform _playerTransform;
        private bool _isMouthOpened = false;

        private void Start() {
            _playerTransform = _playerProvider.Player.transform;
            SetIsMouthOpened(false);
        }

        private void Update() {
            var sqrDistanceToPlayer = Vector3.SqrMagnitude(transform.position - _playerTransform.position);
            var targetDistance = _gameplayConfig.AnimalDetectPlayerDistance;
            
            var shouldOpenMouth = sqrDistanceToPlayer < targetDistance * targetDistance;
            if (shouldOpenMouth == _isMouthOpened) {
                return;
            }
            
            SetIsMouthOpened(shouldOpenMouth);
        }

        private void SetIsMouthOpened(bool isMouthOpened) {
            _isMouthOpened = isMouthOpened;

            _mouthRoot.SetActive(_isMouthOpened);
            _foodReceiver.SetActive(_isMouthOpened);
        }

        public void Initialize(AnimalFoodReceiver foodReceiver) {
            _foodReceiver = foodReceiver;
        }
    }
}
