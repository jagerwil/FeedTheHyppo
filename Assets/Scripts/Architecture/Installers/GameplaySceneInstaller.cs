using FeedTheHyppo.Architecture.SceneInitializers;
using FeedTheHyppo.Architecture.StateMachine.Gameplay;
using FeedTheHyppo.Gameplay._Factories;
using FeedTheHyppo.Gameplay._Factories.Implementations;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay._Providers.Implementations;
using FeedTheHyppo.Gameplay._Services;
using FeedTheHyppo.Gameplay._Services.Implementations;
using FeedTheHyppo.Gameplay.Animals;
using FeedTheHyppo.Gameplay.Items;
using FeedTheHyppo.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Architecture.Installers {
    public class GameplaySceneInstaller : MonoInstaller {
        [SerializeField] private Transform _playerSpawnRoot;
        [SerializeField] private Transform _foodItemsSpawnRoot;
        [Space]
        [SerializeField] private ItemSpawners _itemSpawners;
        [SerializeField] private Animal _animal;
        [Space]
        [SerializeField] private GameUI _gameUI;

        private IFoodService _foodService;
        
        public override void InstallBindings() {
            BindServices();
            BindProviders();
            BindFactories();
            
            BindGameStates();
            BindInitializer();
        }

        private void BindServices() {
            Container.BindInterfacesTo<PlayerInputService>().AsSingle();
            Container.BindInterfacesTo<FoodService>().AsSingle();
            Container.BindInterfacesTo<ScoreService>().AsSingle();
        }

        private void BindProviders() {
            Container.Bind<IPlayerProvider>().To<PlayerProvider>().AsSingle();

            Container.Bind<IPlayerItemInteractionProvider>()
                     .To<PlayerItemInteractionProvider>()
                     .AsSingle();

            Container.Bind<ISceneObjectsProvider>()
                     .To<SceneObjectsProvider>()
                     .AsSingle().WithArguments(_itemSpawners, _animal);
            
            Container.Bind<IUIProvider>()
                     .To<UIProvider>()
                     .AsSingle().WithArguments(_gameUI);
        }

        private void BindFactories() {
            Container.Bind<IPlayerFactory>()
                     .To<PlayerFactory>()
                     .AsSingle().WithArguments(_playerSpawnRoot);
            
            Container.Bind<IFoodItemFactory>()
                     .To<FoodItemFactory>()
                     .AsSingle().WithArguments(_foodItemsSpawnRoot);
        }

        private void BindGameStates() {
            Container.Bind<GameplayInitializationState>().AsSingle();
            Container.Bind<GameplayMainState>().AsSingle();
            Container.Bind<GameplayGameOverState>().AsSingle();
            Container.Bind<GameplayRestartState>().AsSingle();
        }

        private void BindInitializer() {
            Container.BindInterfacesTo<GameplaySceneInitializer>().AsSingle();
        }
    }
}
