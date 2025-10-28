using FeedTheHyppo.Architecture._Factories;
using FeedTheHyppo.Gameplay._Factories;
using FeedTheHyppo.Gameplay._Providers;
using Jagerwil.Core.Architecture.StateMachine;
using UnityEngine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayRestartState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPlayerFactory _playerFactory;
        private readonly IFoodItemFactory _foodItemFactory;
        private readonly IParticleSystemFactory _particlesFactory;
        private readonly ISceneObjectsProvider _sceneObjectsProvider;

        public GameplayRestartState(IGameStateMachine stateMachine,
            IPlayerFactory playerFactory,
            IFoodItemFactory foodItemFactory,
            IParticleSystemFactory particlesFactory) {
            _stateMachine = stateMachine;
            _playerFactory = playerFactory;
            _foodItemFactory = foodItemFactory;
            _particlesFactory = particlesFactory;
        }
        
        public void Enter() {
            _playerFactory.DespawnAll();
            _foodItemFactory.DespawnAll();
            _particlesFactory.DespawnAll();

            _stateMachine.Enter<GameplayMainState>();
        }
        
        public void Exit() { }
    }
}
