using System;
using R3;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Services {
    public interface IPlayerInputService {
        public ReadOnlyReactiveProperty<Vector2> MoveVector { get; }
        public ReadOnlyReactiveProperty<Vector2> DeltaLookVector { get; }

        public event Action OnInteractButtonPressed;
        
        public void Enable();
        public void Disable();
    }
}
