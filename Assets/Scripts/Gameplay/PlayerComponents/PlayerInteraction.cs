using System;
using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay.Items;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    public class PlayerInteraction : MonoBehaviour {
        #region Serialized & Injected Fields
        [SerializeField] private Transform _equippedItemRoot;
        [SerializeField] private LayerMask _interactionMask;
        [SerializeField] private LayerMask _throwRaycastMask;
        [SerializeField] private float _throwRaycastMaxDistance = 20f;
        
        [Inject] private IPlayerItemInteractionProvider _interactionProvider;
        [Inject] private ISceneObjectsProvider _sceneObjectsProvider;
        [Inject] private PlayerConfig _config;
        #endregion

        #region Private Fields
        private Camera _camera;
        private Collider _collider;
        #endregion
        
        
        #region Unity Callbacks
        private void Update() {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, 
                                out var hit, _config.InteractionDistance, _interactionMask)) {
                var item = hit.transform?.GetComponent<BaseItem>();
                _interactionProvider.SetLookedAtItem(item);
                return;
            }
            
            _interactionProvider.SetLookedAtItem(null);
        }
        #endregion

        #region Public Methods
        public void InjectComponents(Camera cam, Collider col) {
            _camera = cam;
            _collider = col;
        }

        public void Initialize() {
            _interactionProvider.SetLookedAtItem(null);
            _interactionProvider.SetEquippedItem(null);
        }
        
        public void Interact() {
            var lookedAtItem = _interactionProvider.LookedAtItem.CurrentValue;
            var equippedItem = _interactionProvider.EquippedItem.CurrentValue;

            if (equippedItem != null) {
                var throwEndPoint = GetThrowEndPoint();
                equippedItem.Throw(throwEndPoint, _config.ItemThrowForce);
                
                _interactionProvider.SetEquippedItem(null);
                return;
            }

            if (lookedAtItem != null) {
                lookedAtItem.TakeFromPlace(_equippedItemRoot, _collider);
                _interactionProvider.SetEquippedItem(lookedAtItem);
            }
        }
        #endregion

        #region Private Methods
        private Vector3 GetThrowEndPoint() {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward,
                                out var hit, _throwRaycastMaxDistance, _throwRaycastMask)) {
                return hit.point;
            }
            
            return _camera.transform.position + _camera.transform.forward * _throwRaycastMaxDistance;
        }
        #endregion
    }
}
