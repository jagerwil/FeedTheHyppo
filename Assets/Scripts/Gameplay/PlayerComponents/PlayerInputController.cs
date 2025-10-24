using FeedTheHyppo.Gameplay._Services;
using R3;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.PlayerComponents {
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerLookAround), typeof(PlayerInteraction))]
    public class PlayerInputController : MonoBehaviour {
        #region Readonly Fields
        private readonly CompositeDisposable _disposables = new();
        #endregion

        #region Injected Fields
        [Inject] private IPlayerInputService _inputService;
        
        private PlayerMovement _playerMovement;
        private PlayerLookAround _playerLookAround;
        private PlayerInteraction _playerInteraction;
        #endregion

        
        #region Unity Callbacks

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
        
        
        #region Public Methods
        public void InjectComponents(PlayerMovement movement, PlayerLookAround lookAround, 
            PlayerInteraction interaction) {
            _playerMovement = movement;
            _playerLookAround = lookAround;
            _playerInteraction = interaction;
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
            _playerInteraction.Interact();
        }
        #endregion
    }
}
