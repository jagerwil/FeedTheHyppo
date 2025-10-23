using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FeedTheHyppo.Configs {
    [CreateAssetMenu(fileName = "ScenesAddressesConfig", menuName = "Configs/Addresses/Scenes")]
    public class ScenesAddressesConfig : ScriptableObject {
        [field: SerializeField] public AssetReference GameplayScene { get; private set; }
    }
}
