using System;
using Jagerwil.Core.Utils.Spawning;
using UnityEngine;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Configs/Gameplay")]
    public class GameplayConfig : ScriptableObject {
        [field: SerializeField] public FoodInfo FoodInfo { get; private set; }
        [field: Space]
        [field: SerializeField] public AnimalInfo AnimalInfo { get; private set; }
        [field: Space]
        [field: SerializeField] public FoodServiceInfo FoodServiceInfo { get; private set; }
        [field: Space]
        [field: SerializeField] public ScoreServiceInfo ScoreServiceInfo { get; private set; }
    }

    //When individual sub configs (infos) become bigger, i'll move them into their own configs
    [Serializable]
    public class FoodInfo {
        [field: SerializeField] public float MelonFoodIncrease { get; private set; } = 10f;
        [field: SerializeField] public float DelayBeforeSpawning { get; private set; }
        [field: SerializeField] public bool IgnoreFirstSpawnDelay { get; private set; } = true;
    }

    [Serializable]
    public class AnimalInfo {
        [field: SerializeField] public float AnimalDetectPlayerDistance { get; private set; } = 999f;
    }

    [Serializable]
    public class FoodServiceInfo {
        [field: SerializeField] public float MaxFoodValue { get; private set; } = 100f;
        [field: SerializeField] public float DecreaseFoodSpeed { get; private set; } = 10f;
    }

    [Serializable]
    public class ScoreServiceInfo {
        [field: SerializeField] public float ScorePerSecond { get; private set; } = 5f;
        [field: SerializeField] public float ScoreUpdatesInterval { get; private set; } = 1f;
    }
}
