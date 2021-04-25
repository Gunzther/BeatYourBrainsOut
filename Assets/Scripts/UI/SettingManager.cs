using BBO.BBO.GameData;
using BBO.BBO.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace BBO.BBO.InterfaceManagement
{
    public class SettingManager : MonoBehaviour
    {
        [SerializeField]
        private AudioMixerManager audioMixer = default;

        [SerializeField]
        private GameObject menu = default;

        [SerializeField]
        private GameObject desktop = default;

        [SerializeField]
        private Slider master = default;

        [SerializeField]
        private Slider bg = default;

        [SerializeField]
        private Slider fx = default;

        [SerializeField]
        private Camera startCamera = default;

        private void Awake()
        {
            Pause();
            master.value = PlayerPrefs.GetFloat(PlayerPrefsData.MasterVolume);
            bg.value = PlayerPrefs.GetFloat(PlayerPrefsData.BackgroundVolume);
            fx.value = PlayerPrefs.GetFloat(PlayerPrefsData.EffectVolume);
        }

        public void ActiveQuitButton()
        {
            menu.SetActive(true);
            desktop.SetActive(true);
        }

        public void ResetAudioMixer()
        {
            audioMixer.ResetMixer();
            master.value = 0;
            bg.value = 0;
            fx.value = 0;
        }

        public void ToMenu()
        {
            GameManager.Instance.LoadSceneCoroutine("StartGame", null);
            Camera.main.transform.position = startCamera.transform.position;
            Camera.main.transform.rotation = startCamera.transform.rotation;

        }

        public void ToDesktop()
        {
            Application.Quit();
        }

        public void Back()
        {
            Resume();
            Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }

        private void Pause()
        {
            Time.timeScale = 0f;
        }

        private void Resume()
        {
            Time.timeScale = 1f;
        }
    }
}