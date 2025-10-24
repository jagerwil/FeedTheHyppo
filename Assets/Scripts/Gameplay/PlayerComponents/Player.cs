using System;
using FeedTheHyppo.Gameplay._Providers;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    //Main component to interact with outside objects / services
    public class Player : MonoBehaviour, IPoolable {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerLookAround _lookAround;
        [SerializeField] private PlayerInteraction _interaction;
        [SerializeField] private PlayerInputController _inputController;
        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private Camera _camera;
        
        [Inject] private IPlayerProvider _playerProvider;

        private void Awake() {
            _movement.InjectComponents(_rigidbody);
            _lookAround.InjectComponents(_rigidbody, _camera);
            _interaction.InjectComponents(_camera, _collider);
            
            _inputController.InjectComponents(_movement, _lookAround, _interaction);
        }
        
        public void OnSpawned() {
            _movement.Initialize();
            _lookAround.Initialize();
            _interaction.Initialize();
            
            _playerProvider.SetPlayer(this);
        }

        public void OnDespawned() {
            _playerProvider.SetPlayer(null);
        }
    }
}
