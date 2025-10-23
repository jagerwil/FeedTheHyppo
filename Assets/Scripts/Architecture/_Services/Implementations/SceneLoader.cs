using Cysharp.Threading.Tasks;
using FeedTheHyppo.Configs;
using Jagerwil.Core.Services;
using Jagerwil.Core.Services.Implementations;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FeedTheHyppo.Architecture.Services.Implementations {
    public class SceneLoader : BaseSceneLoader<SceneType>, ISceneLoader {
        private readonly ScenesAddressesConfig _scenesAddressesConfig;
        
        public SceneLoader(IAddressablesLoader addressablesLoader,
            ScenesAddressesConfig scenesAddressesConfig)
            : base(addressablesLoader) {
            _scenesAddressesConfig = scenesAddressesConfig;
        }
        
        public override async UniTask LoadAsync(SceneType sceneType) {
            switch (sceneType) {
                case SceneType.Gameplay:
                    await LoadAsyncInternal(_scenesAddressesConfig.GameplayScene);
                    break;
                default:
                    Debug.LogError($"{nameof(SceneLoader)}.{nameof(LoadAsync)}: Scene type {sceneType} is not supported");
                    break;
            }
        }

        protected override AssetReference GetTransitionSceneRef() {
            return null;
        }
    }
}
