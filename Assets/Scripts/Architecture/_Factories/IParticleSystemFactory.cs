using Jagerwil.Core;
using Jagerwil.Core.Architecture.Factories;
using JetBrains.Annotations;
using UnityEngine;

namespace FeedTheHyppo.Architecture._Factories {
    public interface IParticleSystemFactory : IGamePrefabFactory<ParticleSystemWrapper> {
        [CanBeNull] public ParticleSystemWrapper Spawn(ParticleSystemId id, Transform root);
        [CanBeNull] public ParticleSystemWrapper Spawn(ParticleSystemId id, Vector3 position,
            Quaternion rotation, Transform root = null);
    }
}
