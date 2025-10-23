using FeedTheHyppo.Gameplay._Services;
using R3;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.Player {
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerLookAround), typeof(PlayerItemInteraction))]
    public class PlayerInputController : MonoBehaviour {
        #region Readonly Fields
        private readonly CompositeDisposable _disposables = new();
        #endregion

        #region Injected Fields
        [Inject] private IPlayerInputService _inputService;
        
        private PlayerMovement _playerMovement;
        private PlayerLookAround _playerLookAround;
        private PlayerItemInteraction _playerItemInteraction;
        #endregion

        
        #region Unity Callbacks
        private void Awake() {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerLookAround = GetComponent<PlayerLookAround>();
            _playerItemInteraction = GetComponent<PlayerItemInteraction>();
        }

        private void Start() {
            _inputService.MoveVector
                         .Subscribe(MoveVectorChanged)
                         .AddTo(_disposables);
            
            _inputService.DeltaLookVector
                         .Subscribe(DeltaLookVectorChanged)
                         .AddTo(_disposables);
            
            _inputService.OnInteractButtonPressed += InteractButtonPressed;
        }

        private void OnDestroy() {
            if (_inputService != null) {
                _inputService.OnInteractButtonPressed -= InteractButtonPressed;
            }
            _disposables?.Dispose();
        }
        #endregion

        
        #region Private Methods
        private void MoveVectorChanged(Vector2 moveVector) {
            _playerMovement.SetMoveVector(moveVector);
        }

        private void DeltaLookVectorChanged(Vector2 deltaLookVector) {
            _playerLookAround.SetDeltaLookVector(deltaLookVector);
        }

        private void InteractButtonPressed() {
            _playerItemInteraction.Interact();
        }
        #endregion
    }
}
