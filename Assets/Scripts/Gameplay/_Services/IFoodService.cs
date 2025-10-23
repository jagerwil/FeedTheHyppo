using System;
using R3;

namespace FeedTheHyppo.Gameplay._Services {
    public interface IFoodService {
        public ReadOnlyReactiveProperty<float> CurrentFood { get; }
        public float MaxFood { get; }

        public event Action onFoodEnded;

        public void IncreaseFood(float amount);
    }
}
