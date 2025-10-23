using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "ScenesAddressesConfig", menuName = "Configs/Scenes Addresses")]
    public class ScenesAddressesConfig : ScriptableObject {
        [field: SerializeField] public AssetReference GameplayScene { get; private set; }
    }
}
