using System;
using FeedTheHyppo.Configs;
using Jagerwil.Core;
using Jagerwil.Core.Architecture.Factories.Implementations;
using Jagerwil.Core.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace FeedTheHyppo.Architecture._Factories.Implementations {
    public class SoundFactory : BaseGameFactory<AudioSourceWrapper>, ISoundFactory, IDisposable {
        private readonly PrefabAddressesConfig _prefabAddresses;
        private readonly SoundsInfo _soundsInfo;

        public SoundFactory(IInstantiator instantiator,
            IAddressablesLoader addressablesLoader,
            PrefabAddressesConfig prefabAddressesConfig,
            AudioConfig audioConfig,
            Transform defaultRoot)
            : base(instantiator, addressablesLoader,
                   new MemoryPoolSettings(0, int.MaxValue, PoolExpandMethods.OneAtATime), defaultRoot) {
            _prefabAddresses = prefabAddressesConfig;
            _soundsInfo = audioConfig.SoundsInfo;

            AudioSourceWrapper.onDespawnRequested += Despawn;
        }

        public void Dispose() {
            AudioSourceWrapper.onDespawnRequested -= Despawn;
        }
        
        public AudioSourceWrapper Spawn(SoundId id, Transform root = null) {
            if (!root) {
                return Spawn(id, Vector3.zero, Quaternion.identity);
            }
            return Spawn(id, root.position, Quaternion.identity, root);
        }
        
        public AudioSourceWrapper Spawn(SoundId id, Vector3 position, 
            Quaternion rotation, Transform root = null) {
            if (id == SoundId.None) {
                return null;
            }

            var soundInfo = _soundsInfo.GetById(id);
            if (soundInfo == null) {
                return null;
            }

            var audioClip = soundInfo.GetRandomClip();
            if (!audioClip) {
                return null;
            }
            
            var sourceWrapper = CreateInternal(position, rotation, root);
            if (!sourceWrapper) {
                return null;
            }
            
            sourceWrapper.SetDistance(soundInfo.HasDistance, soundInfo.MaxDistance);
            sourceWrapper.SetVolume(soundInfo.Volume);
            sourceWrapper.Play(audioClip);
            return sourceWrapper;
        }

        protected override AssetReferenceGameObject GetAssetReference() {
            return _prefabAddresses.Sound;
        }
    }
}
