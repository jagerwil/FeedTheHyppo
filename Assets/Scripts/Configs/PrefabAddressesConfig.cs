using System;
using System.Collections.Generic;
using System.Linq;
using FeedTheHyppo.Architecture;
using FeedTheHyppo.Gameplay;
using Jagerwil.Core.Utils.Data;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "PrefabAddressesConfig", menuName = "Configs/Addresses/Prefabs")]
    public class PrefabAddressesConfig : ScriptableObject {
        [field: SerializeField] public AssetReferenceGameObject Player { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Melon { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Sound { get; private set; }
        [field: SerializeField] public ParticleSystemsPrefabsInfo ParticleSystems { get; private set; }
    }

    [Serializable]
    public class ParticleSystemsPrefabsInfo {
        [SerializeField] private List<ParticleSystemSpawnInfo> _spawnInfos;
        private LookupTable<ParticleSystemId, ParticleSystemSpawnInfo> _lookupTable;
        
        public IReadOnlyList<AssetReferenceGameObject> Prefabs => _spawnInfos.Select(spawnInfo => spawnInfo.Prefab).ToList();

        [CanBeNull]
        public AssetReferenceGameObject GetPrefabById(ParticleSystemId id) {
            if (_lookupTable == null) {
                _lookupTable = new(_spawnInfos, (spawnInfo) => spawnInfo.Id);
            }
            return _lookupTable.GetElement(id)?.Prefab;
        }
    }

    [Serializable]
    public class ParticleSystemSpawnInfo {
        [field: SerializeField] public ParticleSystemId Id { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Prefab { get; private set; }
    }
}
