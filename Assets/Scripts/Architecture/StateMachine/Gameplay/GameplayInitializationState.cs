using Cysharp.Threading.Tasks;
using FeedTheHyppo.Gameplay._Factories;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using UnityEngine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayInitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly IPlayerFactory _playerFactory;
        private readonly IFoodItemFactory _foodItemFactory;

        public GameplayInitializationState(IGameStateMachine stateMachine, 
            IWindowService windowService,
            IPlayerFactory playerFactory,
            IFoodItemFactory foodItemFactory) {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _playerFactory = playerFactory;
            _foodItemFactory = foodItemFactory;
        }

        public void Enter() {
            _windowService.RegisterAll();
            WarmUpFactoriesAsync().Forget();
        }

        public void Exit() { }

        private async UniTask WarmUpFactoriesAsync() {
            var warmUpPlayerTask = _playerFactory.WarmUpAsync();
            var warmUpItemsTask = _foodItemFactory.WarmUpAsync();

            await UniTask.WhenAll(warmUpPlayerTask, warmUpItemsTask);
            _stateMachine.Enter<GameplayMainState>();
        }
    }
}
