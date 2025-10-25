using FeedTheHyppo.Gameplay.UI;
using UnityEngine;

namespace FeedTheHyppo.Gameplay._Providers.Implementations {
    public class UIProvider : IUIProvider {
        public GameUI GameUI { get; private set; }

        public UIProvider(GameUI gameUI) {
            GameUI = gameUI;
        }
    }
}
