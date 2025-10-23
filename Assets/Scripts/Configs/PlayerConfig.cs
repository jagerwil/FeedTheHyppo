using UnityEngine;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]
    public class PlayerConfig : ScriptableObject {
        [field: Header("Input")]
        [field: SerializeField] public float DefaultLookSensitivity { get; private set; } = 1f;
        [field: Header("Gameplay")]
        [field: SerializeField] public float InteractionDistance { get; private set; } = 2f;
        [field: SerializeField] public float ItemThrowForce { get; private set; } = 10f;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
    }
}
