using FeedTheHyppo.Gameplay.Items;

namespace FeedTheHyppo.Gameplay._Providers.Implementations {
    public class SceneObjectsProvider : ISceneObjectsProvider {
        public ItemSpawners ItemSpawners { get; private set; }

        public SceneObjectsProvider(ItemSpawners itemSpawners) {
            ItemSpawners = itemSpawners;
        }
    }
}
