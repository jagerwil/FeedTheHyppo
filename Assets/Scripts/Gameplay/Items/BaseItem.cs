using System;
using System.Collections.Generic;
using UnityEngine;

namespace FeedTheHyppo.Gameplay.Items {
    public class BaseItem : MonoBehaviour {
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;

        private List<Collider> _ignoredColliders = new();

        private void Awake() {
            
        }

        protected virtual void OnDisable() {
            foreach (var ignoredCollider in _ignoredColliders) {
                Physics.IgnoreCollision(_collider, ignoredCollider, false);
            }
            
            _ignoredColliders.Clear();
        }

        public void PutIntoPlace(Transform place, bool shouldDisablePhysics) {
            transform.SetParent(place);
            transform.localPosition = Vector3.zero;
            
            _collider.enabled = !shouldDisablePhysics;
            _rigidbody.isKinematic = shouldDisablePhysics;
        }

        public void AddIgnoreCollider(Collider ignoreCollider) {
            Physics.IgnoreCollision(_collider, ignoreCollider, true);
            _ignoredColliders.Add(ignoreCollider);
        }
        
        public void Throw(Vector3 endPoint, float throwForce) {
            transform.SetParent(null);
            transform.LookAt(endPoint);
            
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
    }
}
