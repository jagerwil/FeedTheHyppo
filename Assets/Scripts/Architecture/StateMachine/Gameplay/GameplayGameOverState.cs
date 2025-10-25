using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay._Services;
using FeedTheHyppo.Gameplay.Windows;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayGameOverState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IScoreService _scoreService;
        private readonly IWindowService _windowService;
        private readonly IUIProvider _uiProvider;
        
        public GameplayGameOverState(IGameStateMachine stateMachine, IScoreService scoreService, 
            IWindowService windowService, IUIProvider uiProvider) {
            _stateMachine = stateMachine;
            _scoreService = scoreService;
            _windowService = windowService;
            _uiProvider = uiProvider;
        }

        public void Enter() {
            _scoreService.StopCounting();
            _uiProvider.GameUI.HideUI();
            
            var gameOverWindow = _windowService.Open<GameOverWindow>();
            gameOverWindow?.Initialize(RestartGame);
        }

        public void Exit() {
            _windowService.Close<GameOverWindow>();
            
        }

        private void RestartGame() {
            _stateMachine.Enter<GameplayRestartState>();
        }
    }
}
