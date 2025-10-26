using System;
using UnityEngine;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/Audio")]
    public class AudioConfig : ScriptableObject {
        [field: SerializeField] public MusicInfo MusicInfo { get; private set; }
    }

    [Serializable]
    public class MusicInfo {
        [field: SerializeField] public float LowpassCutoffFrequency { get; private set; } = 2200f;
        [field: SerializeField] public float LowpassNormalFrequency { get; private set; } = 12000f;
        [field: SerializeField] public float ChangeFrequencyDuration { get; private set; } = 1f;
    }
}
