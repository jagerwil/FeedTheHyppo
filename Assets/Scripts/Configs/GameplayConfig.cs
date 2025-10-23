using UnityEngine;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/Gameplay")]
    public class GameplayConfig : ScriptableObject {
        [field: SerializeField] public float MelonFoodIncrease { get; private set; }
        [field: SerializeField] public float MelonSpawnInterval { get; private set; }
        [field: Space]
        [field: SerializeField] public float MaxFoodValue { get; private set; }
        [field: SerializeField] public float DecreaseFoodSpeed { get; private set; }
        [field: Space]
        [field: SerializeField] public float AnimalDetectPlayerDistance { get; private set; }
    }
}
