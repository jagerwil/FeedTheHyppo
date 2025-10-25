using UnityEngine;

namespace FeedTheHyppo.Gameplay.UI {
    //In the future if we need specific UIs, we can reference them here
    //and get them via UIProvider
    public class GameUI : MonoBehaviour {
        public void ShowUI() {
            gameObject.SetActive(true);
        }

        public void HideUI() {
            gameObject.SetActive(false);
        }
    }
}
