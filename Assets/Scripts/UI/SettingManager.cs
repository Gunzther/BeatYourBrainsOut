using BBO.BBO.GameManagement;
using BBO.BBO.TeamManagement;
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
            GameManager.Instance.LoadSceneCoroutine("StartGame", () => TeamManager.Instance.Reload());
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