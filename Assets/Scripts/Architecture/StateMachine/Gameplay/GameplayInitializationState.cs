using Jagerwil.Core.Architecture.StateMachine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayInitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;

        public GameplayInitializationState(IGameStateMachine stateMachine) {
            _stateMachine = stateMachine;
        }
        
        public void Enter() {
            _stateMachine.Enter<GameplayMainState>();
        }

        public void Exit() { }
    }
}
