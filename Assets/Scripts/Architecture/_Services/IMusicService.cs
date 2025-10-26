using UnityEngine;

namespace FeedTheHyppo.Architecture._Services {
    public interface IMusicService {
        public void StartMusic();
        
        public void StartMuffle();
        public void StopMuffle();
    }
}
