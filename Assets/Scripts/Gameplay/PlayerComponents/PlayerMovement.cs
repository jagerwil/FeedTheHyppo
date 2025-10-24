using FeedTheHyppo.Configs;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    public class PlayerMovement : MonoBehaviour {
        [Inject] private PlayerConfig _playerConfig;
        
        private Rigidbody _rigidbody;
        private Vector3 _moveVector;

        private void FixedUpdate() {
            _rigidbody.linearVelocity = _rigidbody.rotation * (_moveVector * _playerConfig.MoveSpeed);
        }

        public void InjectComponents(Rigidbody rb) {
            _rigidbody = rb;
        }

        public void Initialize() {
            _moveVector = Vector3.zero;
        }

        public void SetMoveVector(Vector2 moveVector) {
            _moveVector = new Vector3(moveVector.x, 0f, moveVector.y);
        }
    }
}
