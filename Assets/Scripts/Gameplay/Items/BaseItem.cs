using System;
using System.Collections.Generic;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Items {
    public class BaseItem : MonoBehaviour, IPoolable {
        #region Serialized Fields
        [SerializeField] private GameObject _model;
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private bool _despawnOnThrowCollision = true;
        [Header("Optional")]
        [CanBeNull][SerializeField] private ParticleSystemWrapper _destroyParticles;
        
        [Inject] private ISceneObjectsProvider _sceneObjectsProvider;
        #endregion
        
        #region Private Fields
        private Transform _defaultRoot;
        private List<Collider> _ignoredColliders = new();
        #endregion
        
        #region Public Properties
        public ItemState State { get; private set; }
        #endregion

        #region Static Events
        public static event Action<BaseItem> onItemSpawned;
        public static event Action<BaseItem, ItemState> onItemStateChanged;
        public static event Action<BaseItem> onDespawnRequested;
        #endregion
        
        #region Unity Callbacks
        private void OnCollisionEnter(Collision other) {
            if (State == ItemState.Thrown && _despawnOnThrowCollision) {
                StartDespawning();
            }
        }
#endregion
        
        #region Public Methods
        public virtual void OnSpawned() {
            _model.SetActive(true);
            _destroyParticles?.Clear();
            
            SetState(ItemState.Idle);
            
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            
            onItemSpawned?.Invoke(this);
        }

        public virtual void OnDespawned() {
            foreach (var ignoredCollider in _ignoredColliders) {
                Physics.IgnoreCollision(_collider, ignoredCollider, false);
            }
            
            _ignoredColliders.Clear();
        }
        
        public void SetDefaultRoot(Transform defaultRoot) {
            _defaultRoot = defaultRoot;
        }

        public void TakeFromPlace(Transform newPlace, Collider colliderToIgnore = null) {
            transform.SetParent(newPlace);
            transform.localPosition = Vector3.zero;
            SetState(ItemState.InPlace);

            if (colliderToIgnore) {
                AddIgnoreCollider(colliderToIgnore);
            }
            
        }

        public void Throw(Vector3 endPoint, float throwForce) {
            transform.SetParent(_defaultRoot);
            transform.LookAt(endPoint);

            SetState(ItemState.Thrown);
            _rigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }
        #endregion
        
        #region Private Methods
        private void SetState(ItemState state) {
            switch (state) {
                case ItemState.Thrown:
                    _collider.enabled = true;
                    _rigidbody.isKinematic = false;
                    break;
                case ItemState.Idle:
                    _collider.enabled = true;
                    _rigidbody.isKinematic = true;
                    break;
                case ItemState.InPlace:
                case ItemState.Destroyed:
                    _collider.enabled = false;
                    _rigidbody.isKinematic = true;
                    break;
                default:
                    Debug.LogError($"{nameof(BaseItem)}.{nameof(SetState)}(): ItemState \"{state}\" is not supported");
                    return; 
            }
            State = state;
            onItemStateChanged?.Invoke(this, State);
        }
        
        private void AddIgnoreCollider(Collider ignoreCollider) {
            Physics.IgnoreCollision(_collider, ignoreCollider, true);
            _ignoredColliders.Add(ignoreCollider);
        }

        protected void StartDespawning() {
            SetState(ItemState.Destroyed);
            
            if (_destroyParticles) {
                _model.SetActive(false);
                _destroyParticles.Play(InvokeOnDespawnRequested);
                return;
            }

            InvokeOnDespawnRequested();
        }

        private void InvokeOnDespawnRequested() {
            onDespawnRequested?.Invoke(this);
        }
        #endregion
    }

    public enum ItemState {
        Idle = 0,
        InPlace = 1,
        Thrown = 2,
        Destroyed = 3,
    }
}
