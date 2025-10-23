using System;
using FeedTheHyppo.Architecture.StateMachine.Gameplay;
using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture.SceneInitializers {
    public class GameplaySceneInitializer : IInitializable, IDisposable {
        private readonly IGameStateMachine _stateMachine;

        public GameplaySceneInitializer(IGameStateMachine stateMachine,
            GameplayInitializationState initializationState,
            GameplayMainState mainState) {
            _stateMachine = stateMachine;
            
            _stateMachine.Register(initializationState);
            _stateMachine.Register(mainState);
        }

        public void Dispose() {
            _stateMachine?.Unregister<GameplayInitializationState>();
            _stateMachine?.Unregister<GameplayMainState>();
        }
        
        public void Initialize() {
            _stateMachine.Enter<GameplayInitializationState>();
        }
    }
}
