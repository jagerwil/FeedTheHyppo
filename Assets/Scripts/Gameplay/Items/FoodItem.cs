using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Items {
    public class FoodItem : BaseItem, IPoolable {
        [SerializeField] private float _restoredHungerAmount = 10f;
        
    }
}
