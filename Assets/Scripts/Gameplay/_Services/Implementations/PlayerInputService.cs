using System;
using FeedTheHyppo.Configs;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace FeedTheHyppo.Gameplay._Services.Implementations {
    public class PlayerInputService : IPlayerInputService, ITickable {
        #region Readonly Fields
        private readonly PlayerConfig _playerConfig;
        private readonly InputActions _inputActions;

        private readonly ReactiveProperty<Vector2> _moveVector = new();
        private readonly ReactiveProperty<Vector2> _deltaLookVector = new();
        #endregion

        #region Events
        public ReadOnlyReactiveProperty<Vector2> MoveVector => _moveVector;
        public ReadOnlyReactiveProperty<Vector2> DeltaLookVector => _deltaLookVector;
        
        public event Action OnInteractButtonPressed;
        #endregion
        
        
        public PlayerInputService(PlayerConfig playerConfig) {
            _playerConfig = playerConfig;
            _inputActions = new InputActions();

            _inputActions.Player.Move.performed += MovePerformed;
            _inputActions.Player.Move.canceled += MoveCancelled;

            //_inputActions.Player.LookGamepad.performed += ActionPerformed;
            //_inputActions.Player.LookGamepad.canceled += ActionCancelled;

            _inputActions.Player.Interact.performed += InteractPerformed;
        }

        
        #region Public Methods
        public void Tick() {
            var pointerValue = _inputActions.Player.LookPointer.ReadValue<Vector2>();
            _deltaLookVector.Value = pointerValue * _playerConfig.DefaultLookSensitivity;
        }

        public void Enable() {
            _inputActions.Enable();
        }
        
        public void Disable() {
            _inputActions.Disable();
        }
        #endregion

        
        #region Private Methods
        private void MovePerformed(InputAction.CallbackContext ctx) {
            _moveVector.Value = ctx.ReadValue<Vector2>();
        }

        private void MoveCancelled(InputAction.CallbackContext ctx) {
            _moveVector.Value = Vector2.zero;
        }

        private void InteractPerformed(InputAction.CallbackContext ctx) {
            Debug.Log($"<color=#6BFF5E>Interact performed!</color>");
            OnInteractButtonPressed?.Invoke();
        }
        #endregion
    }
}
