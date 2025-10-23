using R3;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Providers.Implementations {
    public class PlayerProvider : IPlayerProvider {
        private readonly ReactiveProperty<PlayerComponents.Player> _player = new();
        
        public ReadOnlyReactiveProperty<PlayerComponents.Player> Player => _player;
        
        public void SetPlayer(PlayerComponents.Player player) {
            _player.Value = player;
        }
    }
}
