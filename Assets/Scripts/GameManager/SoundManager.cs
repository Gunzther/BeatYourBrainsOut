using BBO.BBO.Utilities;
using UnityEngine;

namespace BBO.BBO.GameManagement
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField]
        private AudioSource playerHurtingSound = default;
        [SerializeField]
        private AudioSource playerWalkingSound = default;
        [SerializeField]
        private AudioSource kikiWalkingSound = default;

        public void PlayPlayerHurt()
        {
            playerHurtingSound.Play();
        }

        public void PlayPlayerWalking()
        {
            if (!playerWalkingSound.isPlaying)
            {
                playerWalkingSound.Play();
            }
        }

        public void PlayKikiWalking()
        {
            kikiWalkingSound.Play();
        }
    }

}