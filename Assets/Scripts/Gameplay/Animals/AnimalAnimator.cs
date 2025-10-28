using FeedTheHyppo.Architecture;
using FeedTheHyppo.Architecture._Factories;
using R3;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Animals {
    public class AnimalAnimator : MonoBehaviour {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _isPlayerCloseBoolName = "IsPlayerClose";
        [SerializeField] private string _onStartedEatingTriggerName = "OnStartedEating";
        [Space]
        [SerializeField] private SoundId _chewingSound;
        [SerializeField] private ParticleSystemId _chewingParticles;

        [Inject] private ISoundFactory _soundFactory;
        [Inject] private IParticleSystemFactory _particlesFactory;
        
        private readonly CompositeDisposable _disposables = new();
        private AnimalFoodReceiver _foodReceiver;

        public void InjectComponents(AnimalMouthController mouthController, AnimalFoodReceiver foodReceiver) {
            _foodReceiver = foodReceiver;
            mouthController.IsMouthOpened
                           .Subscribe(IsPlayerClose)
                           .AddTo(_disposables);

            foodReceiver.onFoodReceived += FoodReceived;
        }

        public void PlayChewingSound() {
            _soundFactory.Spawn(_chewingSound, _foodReceiver.transform);
            _particlesFactory.Spawn(_chewingParticles, _foodReceiver.transform);
        }

        private void IsPlayerClose(bool isMouthOpened) {
            _animator.SetBool(_isPlayerCloseBoolName, isMouthOpened);
        }

        private void FoodReceived() {
            _animator.SetTrigger(_onStartedEatingTriggerName);
        }
    }
}
