using BBO.BBO.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace BBO.BBO.InterfaceManagement
{
    public class SettingManager : MonoBehaviour
    {
        [SerializeField]
        AudioMixerManager audioMixer = default;
        [SerializeField]
        GameObject menu = default;
        [SerializeField]
        GameObject desktop = default;
        [SerializeField]
        private Slider master = default;
        [SerializeField]
        private Slider bg = default;
        [SerializeField]
        private Slider fx = default;
        [SerializeField]
        private Camera startCamera = default;

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
            Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }
    }
}