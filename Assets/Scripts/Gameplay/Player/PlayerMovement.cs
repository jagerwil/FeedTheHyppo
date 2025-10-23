using System;
using FeedTheHyppo.Configs;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Player {
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private Rigidbody _rigidbody;
        
        [Inject] private PlayerConfig _playerConfig;
        
        private Vector3 _moveVector;

        private void FixedUpdate() {
            _rigidbody.linearVelocity = _moveVector * _playerConfig.MoveSpeed;
        }

        public void SetMoveVector(Vector2 moveVector) {
            _moveVector = new Vector3(moveVector.x, 0f, moveVector.y);
        }
    }
}
