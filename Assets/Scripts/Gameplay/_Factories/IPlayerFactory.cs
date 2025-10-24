using FeedTheHyppo.Gameplay.PlayerComponents;
using Jagerwil.Core.Architecture.Factories;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Factories {
    public interface IPlayerFactory : IGameFactory<Player> {
        public Player Spawn();
    }
}
