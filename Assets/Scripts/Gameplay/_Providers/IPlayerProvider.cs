using R3;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Providers {
    public interface IPlayerProvider { 
        public ReadOnlyReactiveProperty<PlayerComponents.Player> Player { get; }

        public void SetPlayer(PlayerComponents.Player player);
    }
}
