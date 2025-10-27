using System;
using FeedTheHyppo.Configs;
using Jagerwil.Core;
using Jagerwil.Core.Architecture.Factories.Implementations;
using Jagerwil.Core.Services;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture._Factories.Implementations {
    public class ParticleSystemFactory : BaseGamePrefabFactory<ParticleSystemWrapper>, IParticleSystemFactory, IDisposable {
        private readonly PrefabAddressesConfig _prefabsAddressesConfig;
        
        public ParticleSystemFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            PrefabAddressesConfig prefabsAddressesConfig,
            Transform defaultRoot)
            : base(instantiator, addressablesLoader,
                   new MemoryPoolSettings(0, int.MaxValue, PoolExpandMethods.OneAtATime), defaultRoot) {
            _prefabsAddressesConfig = prefabsAddressesConfig;
            ParticleSystemWrapper.onDespawnRequested += Despawn;
        }

        public void Dispose() {
            ParticleSystemWrapper.onDespawnRequested -= Despawn;
        }

        public ParticleSystemWrapper Spawn(ParticleSystemId id, Transform root) {
            return Spawn(id, root.position, Quaternion.identity, root);
        }
        
        public ParticleSystemWrapper Spawn(ParticleSystemId id, Vector3 position, Quaternion rotation, Transform root = null) {
            if (id == ParticleSystemId.None) {
                return null;
            }

            var prefab = _prefabsAddressesConfig.ParticleSystems.GetPrefabById(id);
            return CreateInternal(prefab, position, rotation, root);
        }
    }
}
