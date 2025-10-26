using System;
using FeedTheHyppo.Configs;
using Jagerwil.Core.Services;
using Jagerwil.Core.UI;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace FeedTheHyppo.Gameplay._Services.Implementations {
    public class PlayerInputService : IPlayerInputService, ITickable {
        #region Readonly Fields
        private readonly IWindowService _windowService;
        private readonly PlayerInputInfo _inputInfo;
        private readonly InputActions _inputActions;

        private readonly ReactiveProperty<Vector2> _moveVector = new();
        private readonly ReactiveProperty<Vector2> _deltaLookVector = new();
        #endregion

        #region Events
        public ReadOnlyReactiveProperty<Vector2> MoveVector => _moveVector;
        public ReadOnlyReactiveProperty<Vector2> DeltaLookVector => _deltaLookVector;
        
        public event Action OnInteractButtonPressed;
        #endregion
        
        
        public PlayerInputService(IWindowService windowService, PlayerConfig playerConfig) {
            _windowService = windowService;
            _inputInfo = playerConfig.InputInfo;
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
            _deltaLookVector.Value = pointerValue * _inputInfo.DefaultLookSensitivity;
        }

        public void Enable() {
            _windowService.onWindowOpened += WindowOpened;
            _windowService.onAllWindowsClosed += AllWindowsClosed;
            
            SetCursorLocked(true);
            _inputActions.Enable();
        }
        
        public void Disable() {
            _inputActions.Disable();
            SetCursorLocked(false);
            
            _windowService.onWindowOpened -= WindowOpened;
            _windowService.onAllWindowsClosed -= AllWindowsClosed;
            
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
            OnInteractButtonPressed?.Invoke();
        }

        private void WindowOpened(BaseWindow window) {
            SetCursorLocked(false);
        }

        private void AllWindowsClosed() {
            SetCursorLocked(true);
        }

        private void SetCursorLocked(bool isLocked) {
            Cursor.visible = !isLocked;
            Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        }
        #endregion
    }
}
