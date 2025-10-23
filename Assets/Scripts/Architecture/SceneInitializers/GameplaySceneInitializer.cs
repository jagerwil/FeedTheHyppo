using System;
using System.Collections.Generic;
using FeedTheHyppo.Architecture.StateMachine.Gameplay;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.UI;
using Zenject;

namespace FeedTheHyppo.Architecture.SceneInitializers {
    public class GameplaySceneInitializer : IInitializable, IDisposable {
        private readonly IGameStateMachine _stateMachine;
        private readonly List<BaseWindow> _windows;

        public GameplaySceneInitializer(IGameStateMachine stateMachine,
            GameplayInitializationState initializationState,
            GameplayMainState mainState,
            GameplayGameOverState gameOverState,
            GameplayRestartState restartState) {
            _stateMachine = stateMachine;
            
            _stateMachine.Register(initializationState);
            _stateMachine.Register(mainState);
            _stateMachine.Register(gameOverState);
            _stateMachine.Register(restartState);
        }

        public void Dispose() {
            _stateMachine?.Unregister<GameplayInitializationState>();
            _stateMachine?.Unregister<GameplayMainState>();
            _stateMachine?.Unregister<GameplayGameOverState>();
            _stateMachine?.Unregister<GameplayRestartState>();
        }
        
        public void Initialize() {
            _stateMachine.Enter<GameplayInitializationState>();
        }
    }
}
