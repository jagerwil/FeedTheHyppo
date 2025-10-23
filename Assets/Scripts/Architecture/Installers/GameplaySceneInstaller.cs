using FeedTheHyppo.Architecture.SceneInitializers;
using FeedTheHyppo.Architecture.StateMachine.Gameplay;
using FeedTheHyppo.Gameplay._Factories;
using FeedTheHyppo.Gameplay._Factories.Implementations;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay._Providers.Implementations;
using FeedTheHyppo.Gameplay._Services.Implementations;
using FeedTheHyppo.Gameplay.Items;
using FeedTheHyppo.Gameplay.PlayerComponents;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace FeedTheHyppo.Architecture.Installers {
    public class GameplaySceneInstaller : MonoInstaller {
        [SerializeField] private Player _player;
        [SerializeField] private Transform _foodItemsDefaultRoot;
        [FormerlySerializedAs("_foodSpawners")] [SerializeField] private ItemSpawners _itemSpawners;
        
        public override void InstallBindings() {
            BindServices();
            BindProviders();
            BindFactories();
            
            BindGameStates();
            BindInitializer();
        }

        private void BindServices() {
            Container.BindInterfacesTo<PlayerInputService>().AsSingle();
        }

        private void BindProviders() {
            Container.Bind<IPlayerProvider>()
                     .To<PlayerProvider>()
                     .AsSingle().WithArguments(_player);

            Container.Bind<IPlayerItemInteractionProvider>()
                     .To<PlayerItemInteractionProvider>()
                     .AsSingle();

            Container.Bind<ISceneObjectsProvider>()
                     .To<SceneObjectsProvider>()
                     .AsSingle().WithArguments(_itemSpawners);
        }

        private void BindFactories() {
            Container.Bind<IFoodItemFactory>().To<FoodItemFactory>().AsSingle().WithArguments(_foodItemsDefaultRoot);
        }

        private void BindGameStates() {
            Container.Bind<GameplayInitializationState>().AsSingle();
            Container.Bind<GameplayMainState>().AsSingle();
        }

        private void BindInitializer() {
            Container.BindInterfacesTo<GameplaySceneInitializer>().AsSingle();
        }
    }
}
