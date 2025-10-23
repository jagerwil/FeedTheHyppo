using UnityEngine;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]
    public class PlayerConfig : ScriptableObject {
        [field: Header("Input")]
        [field: SerializeField] public float DefaultLookSensitivity { get; private set; } = 1f;
        [field: Header("Gameplay")]
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
    }
}
