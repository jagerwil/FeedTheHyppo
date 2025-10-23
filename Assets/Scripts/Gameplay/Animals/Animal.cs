using System;
using UnityEngine;

namespace FeedTheHyppo.Gameplay.Animals {
    public class Animal : MonoBehaviour {
        [SerializeField] private AnimalMouthController _mouthController;
        [SerializeField] private AnimalFoodReceiver _foodReceiver;

        private void Awake() {
            _mouthController.Initialize(_foodReceiver);
        }
    }
}
