using System;
using FeedTheHyppo.Architecture;
using FeedTheHyppo.Architecture._Factories;
using FeedTheHyppo.Configs;
using MafiaGame.Extensions;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private SoundId _walkingSound;
        [SerializeField] private float _intervalBetweenSounds;
        
        [Inject] private ISoundFactory _soundFactory;
        private PlayerMovementInfo _movementInfo;
        private float _timeUntilNextSound;
        
        private Rigidbody _rigidbody;
        private Vector3 _moveVector;
        private bool _isMoving;

        [Inject]
        private void Inject(PlayerConfig playerConfig) {
            _movementInfo = playerConfig.MovementInfo;
        }

        private void Update() {
            if (_timeUntilNextSound > 0f) {
                _timeUntilNextSound -= Time.deltaTime;
            }

            if (!_isMoving || _timeUntilNextSound > 0f) {
                return;
            }
            
            _soundFactory.Spawn(_walkingSound);
            _timeUntilNextSound += _intervalBetweenSounds;
        }

        private void FixedUpdate() {
            _rigidbody.linearVelocity = _rigidbody.rotation * (_moveVector * _movementInfo.MoveSpeed);
        }

        public void InjectComponents(Rigidbody rb) {
            _rigidbody = rb;
        }

        public void Initialize() {
            SetMoveVector(Vector2.zero);
        }

        public void SetMoveVector(Vector2 moveVector) {
            _moveVector = new Vector3(moveVector.x, 0f, moveVector.y);
            _isMoving = !moveVector.ApproximatelyZero();
        }
    }
}
