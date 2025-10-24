using FeedTheHyppo.Gameplay.Animals;
using FeedTheHyppo.Gameplay.Items;

namespace FeedTheHyppo.Gameplay._Providers {
    public interface ISceneObjectsProvider {
        public ItemSpawners ItemSpawners { get; }
        public Animal Animal { get; }
    }
}
