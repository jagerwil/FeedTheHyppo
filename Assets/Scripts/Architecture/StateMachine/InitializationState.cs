using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;

namespace FeedTheHyppo.Architecture.StateMachine {
    public class InitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;

        public InitializationState(IGameStateMachine stateMachine) {
            _stateMachine = stateMachine;
        }
        
        public void Enter() {
            Application.targetFrameRate = 60;
            
            _stateMachine.Enter<SceneLoadingState, SceneType>(SceneType.Gameplay);
        }
        
        public void Exit() { }
    }
}
