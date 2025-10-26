using FeedTheHyppo.Configs;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    public class PlayerMovement : MonoBehaviour {
        private PlayerMovementInfo _movementInfo;
        
        private Rigidbody _rigidbody;
        private Vector3 _moveVector;

        [Inject]
        private void Inject(PlayerConfig playerConfig) {
            _movementInfo = playerConfig.MovementInfo;
        }

        private void FixedUpdate() {
            _rigidbody.linearVelocity = _rigidbody.rotation * (_moveVector * _movementInfo.MoveSpeed);
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
