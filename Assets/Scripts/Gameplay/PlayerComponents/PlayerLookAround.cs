using UnityEngine;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    public class PlayerLookAround : MonoBehaviour {
        [SerializeField] private float _verticalAngleRestraint = 70f;
        
        private Rigidbody _rigidbody;
        private Camera _camera;

        private float _horizontalRotation;
        private float _verticalRotation;

        public void Initialize(Rigidbody rb, Camera cam) {
            _rigidbody = rb;
            _camera = cam;
        }

        public void SetDeltaLookVector(Vector2 deltaLookVector) {
            _horizontalRotation += deltaLookVector.x;
            _verticalRotation += -1f * deltaLookVector.y;
            
            RotateCamera();
            RotatePlayer();
        }

        private void RotateCamera() {
            var cameraAngle = _camera.transform.eulerAngles;
            cameraAngle.x += _verticalRotation;
            if (cameraAngle.x > 180f) {
                cameraAngle.x -= 360f;
            }
            
            cameraAngle.x = Mathf.Clamp(cameraAngle.x, -1f * _verticalAngleRestraint, _verticalAngleRestraint);

            _camera.transform.eulerAngles = cameraAngle;
            _verticalRotation = 0f;
        }

        private void RotatePlayer() {
            var rotation = _rigidbody.rotation.eulerAngles;
            rotation.y += _horizontalRotation;

            _rigidbody.MoveRotation(Quaternion.Euler(rotation));
            //transform.eulerAngles = rotation;
            
            _horizontalRotation = 0f;
        }
    }
}
