using FeedTheHyppo.Gameplay._Providers;
using FeedTheHyppo.Gameplay.Items;
using R3;
using UnityEngine;
using Zenject;
using CompositeDisposable = R3.CompositeDisposable;

namespace FeedTheHyppo.Gameplay.UI {
    public class ThrowHelpMessageView : MonoBehaviour {
        [SerializeField] private GameObject _messageObject;
        
        [Inject] private IPlayerItemInteractionProvider _itemInteractionProvider;

        private readonly CompositeDisposable _disposables = new();

        private void Start() {
            _itemInteractionProvider.EquippedItem.Subscribe(EquippedItemChanged)
                                                 .AddTo(_disposables);
        }

        private void EquippedItemChanged(BaseItem item) {
            var hasEquippedItem = (bool)item;
            
            _messageObject.SetActive(hasEquippedItem);
        }
    }
}
