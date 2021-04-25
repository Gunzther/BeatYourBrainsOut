using BBO.BBO.GameData;
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
            PlayerPrefs.SetFloat(PlayerPrefsData.MasterVolume, value);
        }

        public void SetBackgroundMixer(float value)
        {
            mixer.SetFloat("Background", value);
            PlayerPrefs.SetFloat(PlayerPrefsData.BackgroundVolume, value);
        }

        public void SetEffectMixer(float value)
        {
            mixer.SetFloat("Effect", value);
            PlayerPrefs.SetFloat(PlayerPrefsData.EffectVolume, value);
        }

        public void ResetMixer()
        {
            SetBackgroundMixer(0);
            SetEffectMixer(0);
            SetMasterMixer(0);
        }
    }
}