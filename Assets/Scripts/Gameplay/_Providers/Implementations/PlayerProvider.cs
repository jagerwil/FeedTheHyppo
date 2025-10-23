using FeedTheHyppo.Gameplay.PlayerComponents;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Providers.Implementations {
    public class PlayerProvider : IPlayerProvider {
        public Player Player { get; private set; }
        
        public PlayerProvider(Player player) {
            Player = player;
        }
    }
}
