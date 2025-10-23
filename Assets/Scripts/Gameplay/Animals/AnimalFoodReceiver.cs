using System;
using FeedTheHyppo.Gameplay.Items;
using UnityEngine;

namespace FeedTheHyppo.Gameplay.Animals {
    public class AnimalFoodReceiver : MonoBehaviour {
        [SerializeField] private Collider _collider;

        public void SetActive(bool isActive) {
            _collider.enabled = isActive;
        }
        
        private void OnTriggerEnter(Collider other) {
            var food = other.GetComponent<FoodItem>();
            if (!food) {
                return;
            }
            
            food.IncreaseFood();
        }
    }
}
