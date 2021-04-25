using UnityEngine;
using UnityEngine.UI;

namespace BBO.BBO.InterfaceManagement
{
    public class UIManager : InterfaceManager
    {
        [Header("HP")]
        [SerializeField]
        private Slider teamHpSlider = default;
        [SerializeField]
        private Text teamHpText = default;

        [Header("Result Page")]
        [SerializeField]
        private GameObject resultCanvas = default;

        [Header("Setting")]
        [SerializeField]
        private GameObject setting = default;

        public void SetTeamHpMaxValue(int value)
        {
            teamHpSlider.maxValue = value;
            teamHpText.text = value.ToString();

            SetTeamHpValue(value);
        }

        public void SetTeamHpValue(int value)
        {
            if (value < 0)
            {
                value = 0;
                SetActiveCanvas(resultCanvas, currentCanvas);
            }

            teamHpSlider.value = value;
            teamHpText.text = value.ToString();
        }

        public override void Back()
        {
            Setting();
        }

        public void Setting()
        {
            GameObject tmp = Instantiate(setting);
            SettingManager settingManager = tmp.GetComponent<SettingManager>();
            settingManager?.ActiveQuitButton();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SettingManager settingManager = FindObjectOfType<SettingManager>();

                if (!settingManager)
                {
                    Back();
                }
            }
        }
    }
}