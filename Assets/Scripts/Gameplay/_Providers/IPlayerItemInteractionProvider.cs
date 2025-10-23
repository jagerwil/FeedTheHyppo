using FeedTheHyppo.Gameplay.Items;
using R3;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Providers {
    public interface IPlayerItemInteractionProvider {
        public ReadOnlyReactiveProperty<BaseItem> LookedAtItem { get; }
        public ReadOnlyReactiveProperty<BaseItem> EquippedItem { get; }

        public void SetLookedAtItem(BaseItem item);
        public void SetEquippedItem(BaseItem item);
    }
}
