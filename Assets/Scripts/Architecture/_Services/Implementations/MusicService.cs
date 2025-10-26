using DG.Tweening;
using FeedTheHyppo.Configs;
using Unity.VisualScripting;
using UnityEngine;

namespace FeedTheHyppo.Architecture._Services.Implementations {
    public class MusicService : IMusicService {
        private readonly MusicInfo _musicInfo;
        private readonly AudioSource _audioSource;
        private readonly AudioLowPassFilter _lowPassFilter;

        private readonly float _cutOffChangeSpeed;
        private Tween _lowpassFrequencyTween;

        public MusicService(AudioConfig audioConfig, AudioSource audioSource) {
            _musicInfo = audioConfig.MusicInfo;
            _audioSource = audioSource;
            _lowPassFilter = audioSource.GetOrAddComponent<AudioLowPassFilter>();

            var deltaFrequency = Mathf.Abs(_musicInfo.LowpassNormalFrequency - _musicInfo.LowpassCutoffFrequency);
            _cutOffChangeSpeed = deltaFrequency / _musicInfo.ChangeFrequencyDuration;
        }

        public void StartMusic() {
            SetLowpassFrequency(_musicInfo.LowpassNormalFrequency);
            
            _audioSource.loop = true;
            _audioSource.Play();
        }

        public void StartMuffle() {
            StartLowpassFrequencyTween(_musicInfo.LowpassCutoffFrequency, Ease.OutCubic);
        }
        
        public void StopMuffle() {
            StartLowpassFrequencyTween(_musicInfo.LowpassNormalFrequency, Ease.InCubic);
        }

        private void StartLowpassFrequencyTween(float endFrequency, Ease ease) {
            _lowpassFrequencyTween?.Kill();
            
            var deltaFrequency = Mathf.Abs(_lowPassFilter.cutoffFrequency - endFrequency);
            var duration = deltaFrequency / _cutOffChangeSpeed;

            _lowpassFrequencyTween = DOTween.To(() => _lowPassFilter.cutoffFrequency,
                                                SetLowpassFrequency,
                                                endFrequency,
                                                duration).SetEase(ease);
        }

        private void SetLowpassFrequency(float frequency) {
            _lowPassFilter.cutoffFrequency = frequency;
        }
    }
}
