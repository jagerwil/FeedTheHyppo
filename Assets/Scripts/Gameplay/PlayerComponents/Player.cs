using System;
using UnityEngine;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    //Main component to interact with outside objects / services
    public class Player : MonoBehaviour {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerLookAround _lookAround;
        [SerializeField] private PlayerInteraction _interaction;
        [SerializeField] private PlayerInputController _inputController;
        [Space]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private Camera _camera;

        private void Awake() {
            _movement.Initialize(_rigidbody);
            _lookAround.Initialize(_rigidbody, _camera);
            _interaction.Initialize(_camera, _collider);
            
            _inputController.Initialize(_movement, _lookAround, _interaction);
        }
    }
}
