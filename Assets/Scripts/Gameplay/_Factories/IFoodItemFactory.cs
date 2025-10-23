using FeedTheHyppo.Gameplay.Items;
using Jagerwil.Core.Architecture.Factories;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Factories {
    public interface IFoodItemFactory : IGameFactory<FoodItem> {
        public FoodItem Spawn(Transform spawnPoint);
    }
}
