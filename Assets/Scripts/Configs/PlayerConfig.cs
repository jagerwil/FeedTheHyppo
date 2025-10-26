using System;
using UnityEngine;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player")]
    public class PlayerConfig : ScriptableObject {
        [field: SerializeField] public PlayerInputInfo InputInfo { get; private set; }
        [field: SerializeField] public PlayerMovementInfo MovementInfo { get; private set; }
        [field: SerializeField] public PlayerItemInteractionInfo ItemInteractionInfo { get; private set; }
    }

    [Serializable]
    public class PlayerInputInfo {
        [field: SerializeField] public float DefaultLookSensitivity { get; private set; } = 0.5f;
    }

    [Serializable]
    public class PlayerMovementInfo {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
    }

    [Serializable]
    public class PlayerItemInteractionInfo {
        [field: SerializeField] public float InteractionDistance { get; private set; } = 2f;
        [field: SerializeField] public float ItemThrowForce { get; private set; } = 10f;
    }
}
