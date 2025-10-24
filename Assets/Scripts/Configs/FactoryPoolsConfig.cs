using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "FactoryPoolsConfig", menuName = "Configs/Factory Pools")]
    public class FactoryPoolsConfig : ScriptableObject {
        [field: SerializeField] public MemoryPoolSettings PlayerFactoryPool { get; private set; }
        [field: SerializeField] public MemoryPoolSettings FoodItemFactoryPool { get; private set; }
    }
}
