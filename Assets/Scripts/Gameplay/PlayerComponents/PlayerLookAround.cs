using System;
using UnityEngine;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    public class PlayerLookAround : MonoBehaviour {
        [SerializeField] private float _verticalAngleRestraint = 70f;
        [SerializeField] private float _rotationSmoothFactor = 5f;
        
        private Rigidbody _rigidbody;
        private Camera _camera;

        private Quaternion _targetCameraLocalRotation;
        private Quaternion _targetPlayerRotation;

        public void InjectComponents(Rigidbody rb, Camera cam) {
            _rigidbody = rb;
            _camera = cam;
        }

        public void Initialize() {
            _camera.transform.localRotation = Quaternion.Euler(Vector3.zero);
            
            _targetCameraLocalRotation = _camera.transform.localRotation;
            _targetPlayerRotation = _rigidbody.rotation;
        }

        private void Update() {
            var deltaTime = Time.deltaTime;
            
            var smoothFactor = _rotationSmoothFactor * deltaTime;
            RotateCamera(smoothFactor);
            RotatePlayer(smoothFactor);
        }

        public void SetDeltaLookVector(Vector2 deltaLookVector) {
            _targetPlayerRotation *= Quaternion.AngleAxis(deltaLookVector.x, Vector3.up);

            var cameraRotation = _targetCameraLocalRotation.eulerAngles;
            cameraRotation.x += -1f * deltaLookVector.y;
            if (cameraRotation.x > 180f) {
                cameraRotation.x -= 360f;
            }
            
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, -1f * _verticalAngleRestraint, _verticalAngleRestraint);
            _targetCameraLocalRotation = Quaternion.Euler(cameraRotation);
        }

        private void RotateCamera(float smoothFactor) {
            var cameraLocalRotation = _camera.transform.localRotation;
            var cameraRotation = Quaternion.Lerp(cameraLocalRotation, _targetCameraLocalRotation, smoothFactor);
            _camera.transform.localRotation = cameraRotation;
        }

        private void RotatePlayer(float smoothFactor) {
            var playerCurrentRotation = _rigidbody.rotation;
            var playerRotation = Quaternion.Lerp(playerCurrentRotation, _targetPlayerRotation, smoothFactor);

            _rigidbody.MoveRotation(playerRotation);
        }
    }
}
