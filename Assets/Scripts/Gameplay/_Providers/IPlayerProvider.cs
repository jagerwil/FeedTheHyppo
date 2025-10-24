using FeedTheHyppo.Gameplay.PlayerComponents;
using R3;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Providers {
    public interface IPlayerProvider { 
        public ReadOnlyReactiveProperty<Player> Player { get; }
        
        public void SetPlayer(Player player);
    }
}
