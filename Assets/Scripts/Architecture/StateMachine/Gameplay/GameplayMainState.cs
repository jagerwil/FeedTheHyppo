using FeedTheHyppo.Gameplay._Factories;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay._Services;
using Jagerwil.Core.Architecture.StateMachine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayMainState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        
        private readonly IPlayerInputService _inputService;
        private readonly IFoodService _foodService;
        private readonly IScoreService _scoreService;
        
        private readonly IPlayerFactory _playerFactory;
        
        private readonly ISceneObjectsProvider _sceneObjectsProvider;
        private readonly IUIProvider _uiProvider;
        

        public GameplayMainState(IGameStateMachine stateMachine,
            IPlayerInputService inputService,
            IFoodService foodService,
            IScoreService scoreService,
            IPlayerFactory playerFactory,
            ISceneObjectsProvider sceneObjectsProvider,
            IUIProvider uiProvider) {
            _stateMachine = stateMachine;
            
            _inputService = inputService;
            _foodService = foodService;
            _scoreService = scoreService;
            
            _playerFactory = playerFactory;
            
            _sceneObjectsProvider = sceneObjectsProvider;
            _uiProvider = uiProvider;
        }
        
        public void Enter() {
            _playerFactory.Spawn();
            
            _sceneObjectsProvider.ItemSpawners.Initialize();
            _sceneObjectsProvider.Animal.Initialize();

            _foodService.Initialize();
            _foodService.onFoodEnded += FoodEnded;
            _scoreService.StartCounting();
            
            _uiProvider.GameUI.ShowUI();
            
            _inputService.Enable();
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
