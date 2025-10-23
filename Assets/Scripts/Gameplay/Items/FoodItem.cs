using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay._Services;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Items {
    public class FoodItem : BaseItem {
        [Inject] private IFoodService _foodService;
        [Inject] private GameplayConfig _gameplayConfig;

        public void IncreaseFood() {
            _foodService.IncreaseFood(_gameplayConfig.MelonFoodIncrease);
            InvokeOnDespawnRequested();
        }
    }
}
