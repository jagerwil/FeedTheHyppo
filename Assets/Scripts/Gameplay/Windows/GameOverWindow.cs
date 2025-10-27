using System;
using FeedTheHyppo.Architecture;
using FeedTheHyppo.Architecture._Factories;
using FeedTheHyppo.Gameplay._Services;
using Jagerwil.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FeedTheHyppo.Gameplay.Windows {
    public class GameOverWindow : BaseWindow {
        #region Serialized & Injected fields
        [SerializeField] private string _scorePrefix = "Score: ";
        [SerializeField] private TMP_Text _scoreText;
        [Space]
        [SerializeField] private string _maxScorePrefix = "Max Score: ";
        [SerializeField] private TMP_Text _maxScoreText;
        [Space]
        [SerializeField] private GameObject _maxScoreReachedGlow;
        [SerializeField] private GameObject _maxScoreReachedText;
        [Space]
        [SerializeField] private Button _restartButton;
        [Space]
        [SerializeField] private SoundId _gameOverSound;
        [SerializeField] private SoundId _buttonSound;
        
        [Inject] private IScoreService _scoreService;
        [Inject] private ISoundFactory _soundFactory;
        #endregion
        
        #region Private fields
        private Action _restartButtonPressedCallback;
        #endregion

        
        #region Unity callbacks
        private void Awake() {
            _restartButton.onClick.AddListener(RestartButtonPressed);
        }

        private void OnEnable() {
            _soundFactory.Spawn(_gameOverSound);
        }

#endregion

        
        #region Public methods
        public override void Show() {
            base.Show();
            
            _scoreText.text = _scorePrefix + _scoreService.Score.ToString();

            var isMaxScoreReached = _scoreService.IsMaxScoreReached;

            _maxScoreText.SetActive(!isMaxScoreReached);
            if (!isMaxScoreReached) {
                _maxScoreText.text = _maxScorePrefix + _scoreService.MaxScore.ToString();
            }
            
            _maxScoreReachedGlow.SetActive(isMaxScoreReached);
            _maxScoreReachedText.SetActive(isMaxScoreReached);
        }

        public void Initialize(Action restartGameAction) {
            _restartButtonPressedCallback = restartGameAction;
        }
        #endregion

        
        #region Private methods
        private void RestartButtonPressed() {
            _soundFactory.Spawn(_buttonSound);
            _restartButtonPressedCallback?.Invoke();
        }
        #endregion
    }
}
