using FeedTheHyppo.Gameplay.PlayerComponents;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Providers {
    public interface IPlayerProvider { 
        public Player Player { get; }
    }
}
