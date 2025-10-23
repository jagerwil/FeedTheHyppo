using FeedTheHyppo.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayMainState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerInputService _inputService;

        public GameplayMainState(IGameStateMachine stateMachine,
            IPlayerInputService inputService) {
            _stateMachine = stateMachine;
            _inputService = inputService;
        }
        
        public void Enter() {
            _inputService.Enable();
        }

        public void Exit() {
            _inputService.Disable();
        }
    }
}
