using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayMainState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerInputService _inputService;
        private readonly IFoodService _foodService;
        private readonly ISceneObjectsProvider _sceneObjectsProvider;

        public GameplayMainState(IGameStateMachine stateMachine,
            IPlayerInputService inputService,
            ISceneObjectsProvider sceneObjectsProvider,
            IFoodService foodService) {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _sceneObjectsProvider = sceneObjectsProvider;
            _foodService = foodService;
        }
        
        public void Enter() {
            _sceneObjectsProvider.ItemSpawners.Initialize();
            _inputService.Enable();

            _foodService.onFoodEnded += FoodEnded;
        }

        public void Exit() {
            _inputService?.Disable();

            if (_foodService != null) {
                _foodService.onFoodEnded -= FoodEnded;
            }
        }

        private void FoodEnded() {
            _stateMachine.Enter<GameplayGameOverState>();
        }
    }
}
