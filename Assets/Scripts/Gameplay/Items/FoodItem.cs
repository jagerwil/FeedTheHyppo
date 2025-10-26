using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay._Services;
using Zenject;

namespace FeedTheHyppo.Gameplay.Items {
    public class FoodItem : BaseItem {
        [Inject] private IFoodService _foodService;
        private FoodInfo _foodInfo;

        [Inject]
        private void Inject(GameplayConfig gameplayConfig) {
            _foodInfo = gameplayConfig.FoodInfo;
        }

        public void IncreaseFood() {
            _foodService.IncreaseFood(_foodInfo.MelonFoodIncrease);
            StartDespawning();
        }
    }
}
