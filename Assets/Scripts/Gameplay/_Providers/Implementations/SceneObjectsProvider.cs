using FeedTheHyppo.Gameplay.Animals;
using FeedTheHyppo.Gameplay.Items;

namespace FeedTheHyppo.Gameplay._Providers.Implementations {
    public class SceneObjectsProvider : ISceneObjectsProvider {
        public ItemSpawners ItemSpawners { get; private set; }
        public Animal Animal { get; private set; }

        public SceneObjectsProvider(ItemSpawners itemSpawners, Animal animal) {
            ItemSpawners = itemSpawners;
            Animal = animal;
        }
    }
}
