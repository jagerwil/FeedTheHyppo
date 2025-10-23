using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayMainState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerInputService _inputService;
        private readonly ISceneObjectsProvider _sceneObjectsProvider;

        public GameplayMainState(IGameStateMachine stateMachine,
            IPlayerInputService inputService,
            ISceneObjectsProvider sceneObjectsProvider) {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _sceneObjectsProvider = sceneObjectsProvider;
        }
        
        public void Enter() {
            _sceneObjectsProvider.ItemSpawners.Initialize();
            _inputService.Enable();
        }

        public void Exit() {
            _inputService.Disable();
        }
    }
}
