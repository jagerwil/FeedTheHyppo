using System;
using DG.Tweening;
using UnityEngine;

namespace FeedTheHyppo.Gameplay.UI {
    public class MaxScoreGlowAnimation : MonoBehaviour {
        [SerializeField] private float _scaleAnimMaxValue = 1.1f;
        [SerializeField] private float _scaleAnimDuration;
        [SerializeField] private float _rotationSpeed;

        private Tween _scaleTween;
        private Tween _rotationTween;

        private void OnEnable() {
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;

            var targetScale = new Vector3(_scaleAnimMaxValue, _scaleAnimMaxValue, _scaleAnimMaxValue);
            _scaleTween = transform.DOScale(targetScale, _scaleAnimDuration * 0.5f)
                                   .SetEase(Ease.InOutCubic)
                                   .SetLoops(-1, LoopType.Yoyo);
            
            var targetRotation = new Vector3(0f, 0f, _rotationSpeed);
            _rotationTween = transform.DOLocalRotate(targetRotation, 1f)
                                      .SetEase(Ease.Linear)
                                      .SetLoops(-1, LoopType.Incremental);
        }

        private void OnDisable() {
            _scaleTween?.Kill();
            _rotationTween?.Kill();

            _scaleTween = null;
            _rotationTween = null;
        }
    }
}
