using FeedTheHyppo.Gameplay.PlayerComponents;
using R3;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Providers.Implementations {
    public class PlayerProvider : IPlayerProvider {
        private readonly ReactiveProperty<Player> _player = new();

        public ReadOnlyReactiveProperty<Player> Player => _player;

        public void SetPlayer(Player player) {
            _player.Value = player;
        }
    }
}
