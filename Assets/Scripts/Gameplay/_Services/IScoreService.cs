using R3;

namespace FeedTheHyppo.Gameplay._Services {
    public interface IScoreService {
        public ReadOnlyReactiveProperty<int> Score { get; }
        public int MaxScore { get; }
        public bool IsMaxScoreReached { get; }
        
        public void StartCounting();
        public void StopCounting();
    }
}
