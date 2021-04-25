using UnityEngine;
using UnityEngine.Audio;

namespace BBO.BBO.InterfaceManagement
{
    public class AudioMixerManager : MonoBehaviour
    {
        [SerializeField]
        private AudioMixer mixer = default;

        public void SetMasterMixer(float value)
        {
            mixer.SetFloat("Master", value);
        }

        public void SetBackgroundMixer(float value)
        {
            mixer.SetFloat("Background", value);
        }

        public void SetEffectMixer(float value)
        {
            mixer.SetFloat("Effect", value);
        }

        public void ResetMixer()
        {
            SetBackgroundMixer(0);
            SetEffectMixer(0);
            SetMasterMixer(0);
        }
    }
}