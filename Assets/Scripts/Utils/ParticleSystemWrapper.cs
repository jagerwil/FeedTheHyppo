using System;
using UnityEngine;

namespace FeedTheHyppo.Utils {
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemWrapper : MonoBehaviour {
        [SerializeField] private ParticleSystem _particleSystem;
        
        private bool _wasPlayingLastFrame;
        private Action _onStoppedPlaying;
        
        private void Update() {
            var isPlaying = _particleSystem.isPlaying;
            if (!isPlaying && _wasPlayingLastFrame) {
                _onStoppedPlaying?.Invoke();
            }
            
            _wasPlayingLastFrame = isPlaying;
        }

        public void Play(Action onStoppedPlaying) {
            _onStoppedPlaying = onStoppedPlaying;
            _particleSystem.Play();
        }

        public void Clear() {
            _particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
