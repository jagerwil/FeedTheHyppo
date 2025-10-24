using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "PrefabAddressesConfig", menuName = "Configs/Addresses/Prefabs")]
    public class PrefabsAddressesConfig : ScriptableObject {
        [field: SerializeField] public AssetReferenceGameObject Player { get; private set; }
        [field: SerializeField] public AssetReferenceGameObject Melon { get; private set; }
    }
}
