using Cysharp.Threading.Tasks;
using FeedTheHyppo.Architecture._Factories;
using FeedTheHyppo.Architecture._Services;
using FeedTheHyppo.Configs;
using FeedTheHyppo.Gameplay._Factories;
using Jagerwil.Core.Architecture.StateMachine;
using Jagerwil.Core.Services;
using UnityEngine;

namespace FeedTheHyppo.Architecture.StateMachine.Gameplay {
    public class GameplayInitializationState : IGameState {
        private readonly IGameStateMachine _stateMachine;
        
        private readonly IWindowService _windowService;
        private readonly IMusicService _musicService;
        
        private readonly IPlayerFactory _playerFactory;
        private readonly IFoodItemFactory _foodItemFactory;
        private readonly ISoundFactory _soundFactory;
        private readonly IParticleSystemFactory _particleSystemFactory;
        
        private readonly PrefabAddressesConfig _prefabsAddressesConfig;

        public GameplayInitializationState(IGameStateMachine stateMachine, 
            IWindowService windowService,
            IMusicService musicService,
            IPlayerFactory playerFactory,
            IFoodItemFactory foodItemFactory,
            ISoundFactory soundFactory,
            IParticleSystemFactory particleSystemFactory,
            PrefabAddressesConfig prefabsAddressesConfig) {
            _stateMachine = stateMachine;
            
            _windowService = windowService;
            _musicService = musicService;
            
            _playerFactory = playerFactory;
            _foodItemFactory = foodItemFactory;
            _soundFactory = soundFactory;
            _particleSystemFactory = particleSystemFactory;
            
            _prefabsAddressesConfig = prefabsAddressesConfig;
        }

        public void Enter() {
            _musicService.StartMusic();
            
            _windowService.RegisterAll();
            WarmUpFactoriesAsync().Forget();
        }

        public void Exit() { }

        private async UniTask WarmUpFactoriesAsync() {
            var warmUpPlayerTask = _playerFactory.WarmUpAsync();
            var warmUpItemsTask = _foodItemFactory.WarmUpAsync();
            var warmUpAudioSourcesTask = _soundFactory.WarmUpAsync();

            var particlePrefabs = _prefabsAddressesConfig.ParticleSystems.Prefabs;
            var warmUpParticlesTask = _particleSystemFactory.WarmUpAsync(particlePrefabs);

            await UniTask.WhenAll(warmUpPlayerTask, warmUpItemsTask, warmUpAudioSourcesTask, warmUpParticlesTask);
            
            _stateMachine.Enter<GameplayMainState>();
        }
    }
}
