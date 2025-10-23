using System;
using System.Collections.Generic;
using FeedTheHyppo.Gameplay._Providers;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Items {
    public class BaseItem : MonoBehaviour, IPoolable {
        #region Serialized Fields
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private bool _despawnOnThrowCollision = true;
        
        [Inject] private ISceneObjectsProvider _sceneObjectsProvider;
        #endregion
        
        #region Private Fields
        private Transform _defaultRoot;
        private List<Collider> _ignoredColliders = new();
        private bool _isThrown;
        #endregion

        #region Static Events
        public static event Action<BaseItem> onItemSpawned;
        public static event Action<BaseItem> onItemTaken;
        public static event Action<BaseItem> onDespawnRequested;
        #endregion
        
        #region Unity Callbacks
        private void OnCollisionEnter(Collision other) {
            if (_isThrown) {
                onDespawnRequested?.Invoke(this);
            }
        }
#endregion
        
        #region Public Methods
        public virtual void OnSpawned() {
            onItemSpawned?.Invoke(this);
        }

        public virtual void OnDespawned() {
            foreach (var ignoredCollider in _ignoredColliders) {
                Physics.IgnoreCollision(_collider, ignoredCollider, false);
            }
            
            _ignoredColliders.Clear();
            _isThrown = false;
        }
        
        public void SetDefaultRoot(Transform defaultRoot) {
            _defaultRoot = defaultRoot;
        }

        public void TakeFromPlace(Transform newPlace, Collider colliderToIgnore = null) {
            transform.SetParent(newPlace);
            transform.localPosition = Vector3.zero;
            
            _collider.enabled = false;
            _rigidbody.isKinematic = true;

            if (colliderToIgnore) {
                AddIgnoreCollider(colliderToIgnore);
            }
            
            onItemTaken?.Invoke(this);
        }

        public void Throw(Vector3 endPoint, float throwForce) {
            transform.SetParent(_defaultRoot);
            transform.LookAt(endPoint);
            
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);

            _isThrown = true;
        }
        #endregion
        
        #region Private Methods
        private void AddIgnoreCollider(Collider ignoreCollider) {
            Physics.IgnoreCollision(_collider, ignoreCollider, true);
            _ignoredColliders.Add(ignoreCollider);
        }
        #endregion
    }
}
