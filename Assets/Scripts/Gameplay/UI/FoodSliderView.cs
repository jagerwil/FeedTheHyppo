using System;
using FeedTheHyppo.Gameplay._Services;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FeedTheHyppo.Gameplay.UI {
    public class FoodSliderView : MonoBehaviour {
        [SerializeField] private Slider _slider;
        
        [Inject] private IFoodService _foodService;
        
        private readonly CompositeDisposable _disposables = new();

        private void Start() {
            _foodService.CurrentFood.Subscribe(CurrentFoodValueChanged).AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables?.Dispose();
        }

        private void CurrentFoodValueChanged(float currentFoodValue) {
            _slider.value = currentFoodValue / _foodService.MaxFood;
        }
    }
}
