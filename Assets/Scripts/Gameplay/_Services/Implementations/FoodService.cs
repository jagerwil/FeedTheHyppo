using System;
using FeedTheHyppo.Configs;
using R3;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay._Services.Implementations {
    public class FoodService : IFoodService, ITickable {
        private readonly ReactiveProperty<float> _currentFood = new();
        private readonly FoodServiceInfo _info;
        
        public ReadOnlyReactiveProperty<float> CurrentFood => _currentFood;
        public float MaxFood { get; private set; }
        
        public event Action onFoodEnded;

        public FoodService(GameplayConfig gameplayConfig) {
            _info = gameplayConfig.FoodServiceInfo;
            
            MaxFood = _info.MaxFoodValue;
        }

        public void Initialize() {
            SetFood(MaxFood);
        }

        public void IncreaseFood(float amount) {
            SetFood(_currentFood.Value + amount);
        }

        public void Tick() {
            SetFood(_currentFood.Value - _info.DecreaseFoodSpeed * Time.deltaTime);
        }

        private void SetFood(float foodValue) {
            var newFoodValue = Mathf.Clamp(foodValue, 0f, MaxFood);

            var isFoodEnded = newFoodValue <= 0f && _currentFood.Value > 0f;
            _currentFood.Value = newFoodValue;

            if (isFoodEnded) {
                onFoodEnded?.Invoke();
            }
        }
    }
}
