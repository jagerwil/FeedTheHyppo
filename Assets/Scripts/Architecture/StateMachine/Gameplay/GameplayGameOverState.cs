using FeedTheHyppo.Gameplay.Windows;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayGameOverState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowService _windowService;

        public GameplayGameOverState(IGameStateMachine stateMachine, IWindowService windowService) {
            _stateMachine = stateMachine;
            _windowService = windowService;
        }

        public void Enter() {
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
