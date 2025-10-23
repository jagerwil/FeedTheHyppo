using System;
using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay.Items;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    public class PlayerInteraction : MonoBehaviour {
        [SerializeField] private Transform _equippedItemRoot;
        [SerializeField] private LayerMask _interactionMask;
        [SerializeField] private LayerMask _throwRaycastMask;
        [SerializeField] private float _throwRaycastMaxDistance = 20f;
        
        [Inject] private IPlayerItemInteractionProvider _interactionProvider;
        [Inject] private PlayerConfig _config;

        private Camera _camera;
        private Collider _collider;

        public void Initialize(Camera cam, Collider col) {
            _camera = cam;
            _collider = col;
        }
        
        private void Update() {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, 
                                out var hit, _config.InteractionDistance, _interactionMask)) {
                var item = hit.transform?.GetComponent<BaseItem>();
                _interactionProvider.SetLookedAtItem(item);
                return;
            }
            
            _interactionProvider.SetLookedAtItem(null);
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
                _interactionProvider.SetEquippedItem(lookedAtItem);
                lookedAtItem.AddIgnoreCollider(_collider);
                lookedAtItem.PutIntoPlace(_equippedItemRoot, true);
            }
        }

        private Vector3 GetThrowEndPoint() {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward,
                                out var hit, _throwRaycastMaxDistance, _throwRaycastMask)) {
                return hit.point;
            }
            
            return _camera.transform.position + _camera.transform.forward * _throwRaycastMaxDistance;
        }
    }
}
