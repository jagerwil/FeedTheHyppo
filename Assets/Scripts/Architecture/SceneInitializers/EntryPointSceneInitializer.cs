using FeedTheHyppo.Architecture.StateMachine;
using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture.SceneInitializers {
    public class EntryPointSceneInitializer : IInitializable {
        private readonly IGameStateMachine _stateMachine;

        public EntryPointSceneInitializer(IGameStateMachine stateMachine,
            InitializationState initializationState,
             SceneLoadingState sceneLoadingState) {
            _stateMachine = stateMachine;
            
            stateMachine.Register(initializationState);
            stateMachine.Register(sceneLoadingState);
        }
        
        public void Initialize() {
            _stateMachine.Enter<InitializationState>();
        }
    }
}
