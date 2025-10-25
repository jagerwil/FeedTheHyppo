using FeedTheHyppo.Gameplay._Services;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.UI {
    public class ScoreView : MonoBehaviour {
        [SerializeField] private string _scorePrefix = "Score: ";
        [SerializeField] private TMP_Text _scoreText;
        
        [Inject] private IScoreService _scoreService;

        private readonly CompositeDisposable _disposables = new();

        private void Start() {
            _scoreService.Score.Subscribe(ScoreChanged).AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables?.Dispose();
        }

        private void ScoreChanged(int score) {
            Debug.Log($"Score changed to {score}!");
            _scoreText.text = _scorePrefix + score.ToString();
        }
    }
}
