using System;
using UnityEngine;

namespace FeedTheHyppo.Gameplay.Items {
    public class ItemThrowTrail : MonoBehaviour {
        [SerializeField] private BaseItem _item;
        [SerializeField] private TrailRenderer _trail;

        private void OnEnable() {
            BaseItem.onItemStateChanged += ItemStateChanged;
            ItemStateChanged(_item, _item.State);
        }

        private void OnDisable() {
            BaseItem.onItemStateChanged -= ItemStateChanged;
        }

        private void ItemStateChanged(BaseItem item, ItemState state) {
            if (item != _item) {
                return;
            }

            var wasEnabled = _trail.enabled;
            var shouldEnable = state == ItemState.Thrown;
            if (wasEnabled && !shouldEnable) {
                _trail.Clear();
            }
            
            _trail.enabled = shouldEnable;
        }
    }
}
