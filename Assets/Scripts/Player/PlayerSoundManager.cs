using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerSoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource playerHurtingSound = default;
        [SerializeField]
        private AudioSource walkingSound = default;

        public void PlayPlayerHurt()
        {
            playerHurtingSound.Play();
        }

        public void PlayPlayerWalking()
        {
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
    }
}
