using UnityEngine;

namespace FeedTheHyppo.Gameplay.Animals {
    public class Animal : MonoBehaviour {
        [SerializeField] private AnimalMouthController _mouthController;
        [SerializeField] private AnimalFoodReceiver _foodReceiver;
        [SerializeField] private AnimalAnimator _animalAnimator;

        private void Awake() {
            _mouthController.InjectComponents(_foodReceiver);
            _animalAnimator.InjectComponents(_mouthController, _foodReceiver);
        }

        public void Initialize() {
            _mouthController.Initialize();
        }
    }
}
