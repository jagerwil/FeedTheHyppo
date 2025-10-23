using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayRestartState : IGameState {
        private readonly IGameStateMachine _stateMachine;

        public GameplayRestartState(IGameStateMachine stateMachine) {
            _stateMachine = stateMachine;
        }
        
        public void Enter() {
            Debug.Log("TODO: RESTART GAME");
            _stateMachine.Enter<GameplayMainState>();
        }
        
        public void Exit() { }
    }
}
