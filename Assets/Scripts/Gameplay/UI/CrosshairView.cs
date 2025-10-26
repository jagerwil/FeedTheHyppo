using System;
using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay.Items;
using R3;
using UnityEngine;
using Zenject;

namespace FeedTheHyppo.Gameplay.UI {
    public class CrosshairView : MonoBehaviour {
        [SerializeField] private GameObject _defaultCrosshair;
        [SerializeField] private GameObject _interactCrosshair;
        
        [Inject] private IPlayerItemInteractionProvider _itemInteractionProvider;

        private readonly CompositeDisposable _disposables = new();

        private void Start() {
            _itemInteractionProvider.LookedAtItem.Subscribe(LookedAtItemChanged)
                                                 .AddTo(_disposables);
        }

        private void LookedAtItemChanged(BaseItem item) {
            var hasInteraction = (bool)item;
            
            _defaultCrosshair.SetActive(!hasInteraction);
            _interactCrosshair.SetActive(hasInteraction);
        }
    }
}
