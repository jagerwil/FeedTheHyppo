using FeedTheHyppo.Gameplay.Items;
using R3;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Providers.Implementations {
    public class PlayerItemInteractionProvider : IPlayerItemInteractionProvider {
        private readonly ReactiveProperty<BaseItem> _lookedAtItem = new();
        private readonly ReactiveProperty<BaseItem> _equippedItem = new();

        public ReadOnlyReactiveProperty<BaseItem> LookedAtItem => _lookedAtItem;
        public ReadOnlyReactiveProperty<BaseItem> EquippedItem => _equippedItem;

        public void SetLookedAtItem(BaseItem item) {
            _lookedAtItem.Value = item;
        }

        public void SetEquippedItem(BaseItem item) {
            _equippedItem.Value = item;
        }
    }
}
