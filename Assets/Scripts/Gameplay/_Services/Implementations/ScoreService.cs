using FeedTheHyppo.Configs;
using R3;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay._Services.Implementations {
    public class ScoreService : IScoreService, ITickable {
        private const string MaxScoreSaveKey = "MaxScore";
        private readonly ReactiveProperty<int> _score = new();
        
        private readonly GameplayConfig _config;

        private float _timeUntilNextScore;
        private float _unclampedScore;
        private bool _isCounting;
        
        public ReadOnlyReactiveProperty<int> Score => _score;
        public int MaxScore { get; private set; }
        public bool IsMaxScoreReached { get; private set; }

        public ScoreService(GameplayConfig config) {
            _config = config;
            
            //TODO: Move to specific SaveLoadService when have more data to save / load
            MaxScore = PlayerPrefs.GetInt(MaxScoreSaveKey);
        }
        
        public void StartCounting() {
            SetUnclampedScore(0f);
            IsMaxScoreReached = false;

            _timeUntilNextScore = _config.ScoreUpdatesInterval;
            _isCounting = true;
        }
        
        public void StopCounting() {
            _isCounting = false;
            
            IsMaxScoreReached = Score.CurrentValue > MaxScore;
            if (IsMaxScoreReached) {
                SetMaxScore(Score.CurrentValue);
            }
        }

        public void Tick() {
            if (!_isCounting) {
                return;
            }

            var deltaTime = Time.deltaTime;
            _timeUntilNextScore -= deltaTime;

            if (_timeUntilNextScore > 0f) {
                return;
            }
            
            _timeUntilNextScore += _config.ScoreUpdatesInterval;
            SetUnclampedScore(_unclampedScore + _config.ScorePerSecond * _config.ScoreUpdatesInterval);
        }

        private void SetUnclampedScore(float unclampedScore) {
            _unclampedScore = unclampedScore;
            _score.Value = Mathf.RoundToInt(_unclampedScore);
        }

        private void SetMaxScore(int score) {
            MaxScore = score;
            PlayerPrefs.SetInt(MaxScoreSaveKey, score);
        }
    }
}
