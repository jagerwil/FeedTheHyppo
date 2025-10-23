using System.Threading;
using Cysharp.Threading.Tasks;
using FeedTheHyppo.Gameplay._Factories;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using UnityEngine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayInitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly IFoodItemFactory _foodItemFactory;

        public GameplayInitializationState(IGameStateMachine stateMachine, 
            IWindowService windowService,
            IFoodItemFactory foodItemFactory) {
            _stateMachine = stateMachine;
            _windowService = windowService;
            _foodItemFactory = foodItemFactory;
        }

        public void Enter() {
            _windowService.RegisterAll();
            WarmUpFactoriesAsync().Forget();
        }

        public void Exit() { }

        private async UniTask WarmUpFactoriesAsync() {
            await _foodItemFactory.WarmUpAsync();
            
            _stateMachine.Enter<GameplayMainState>();
        }
    }
}
