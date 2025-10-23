using System;
using Jagerwil.Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace FeedTheHyppo.Gameplay.Windows {
    public class GameOverWindow : BaseWindow {
        [SerializeField] private Button _restartButton;
        
        private Action _restartButtonPressedCallback;

        private void Awake() {
            _restartButton.onClick.AddListener(RestartButtonPressed);
        }

        public void Initialize(Action restartGameAction) {
            _restartButtonPressedCallback = restartGameAction;
        }

        private void RestartButtonPressed() {
            _restartButtonPressedCallback?.Invoke();
        }
    }
}
