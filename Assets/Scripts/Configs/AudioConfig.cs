using System;
using System.Collections.Generic;
using FeedTheHyppo.Architecture;
using Jagerwil.Core.Utils.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/Audio")]
    public class AudioConfig : ScriptableObject {
        [field: SerializeField] public MusicInfo MusicInfo { get; private set; }
        [field: SerializeField] public SoundsInfo SoundsInfo { get; private set; }
    }

    [Serializable]
    public class MusicInfo {
        [field: SerializeField] public float LowpassCutoffFrequency { get; private set; } = 2200f;
        [field: SerializeField] public float LowpassNormalFrequency { get; private set; } = 12000f;
        [field: SerializeField] public float ChangeFrequencyDuration { get; private set; } = 1f;
    }

    [Serializable]
    public class SoundsInfo {
        [SerializeField] private List<SoundInfo> _sounds;
        
        private LookupTable<SoundId, SoundInfo> _lookupTable;

        [CanBeNull]
        public SoundInfo GetById(SoundId id) {
            if (_lookupTable == null) {
                _lookupTable = new(_sounds, info => info.Id);
            }

            return _lookupTable.GetElement(id);
        }
    }

    [Serializable]
    public class SoundInfo {
        [field: SerializeField] public SoundId Id { get; private set; }
        [SerializeField] private List<AudioClip> _clips;
        [field: Range(0f, 1f)]
        [field: SerializeField] public float Volume { get; private set; } = 1f;
        [field: SerializeField] public bool HasDistance { get; private set; } = true;
        [field: SerializeField] public float MaxDistance { get; private set; } = 30f;
        
        [CanBeNull]
        public AudioClip GetRandomClip() {
            if (_clips.Count == 0) {
                Debug.LogError($"{nameof(SoundInfo)}.{nameof(GetRandomClip)}() Cannot get a random clip, clips amount is 0!");
                return null;
            }
            return _clips[UnityEngine.Random.Range(0, _clips.Count)];
        }
    }
}
