using Jagerwil.Core;
using Jagerwil.Core.Architecture.Factories;
using JetBrains.Annotations;
using UnityEngine;

namespace FeedTheHyppo.Architecture._Factories {
    public interface ISoundFactory : IGameFactory<AudioSourceWrapper> {
        [CanBeNull]
        public AudioSourceWrapper Spawn(SoundId id, Transform root = null);
        
        [CanBeNull]
        public AudioSourceWrapper Spawn(SoundId id, Vector3 position, 
            Quaternion rotation, Transform root = null);
    }
}
