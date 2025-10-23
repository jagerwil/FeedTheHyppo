using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FeedTheHyppo.Gameplay._Factories;
using Jagerwil.Core.Architecture.StateMachine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayInitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IFoodItemFactory _foodItemFactory;

        public GameplayInitializationState(IGameStateMachine stateMachine, 
            IFoodItemFactory foodItemFactory) {
            _stateMachine = stateMachine;
            _foodItemFactory = foodItemFactory;
        }

        public void Enter() {
            WarmUpFactoriesAsync().Forget();
        }

        public void Exit() { }

        private async UniTask WarmUpFactoriesAsync() {
            await _foodItemFactory.WarmUpAsync();
            
            _stateMachine.Enter<GameplayMainState>();
        }
    }
}
