using BBO.BBO.InterfaceManagement;
using UnityEngine;
using UnityEngine.UI;

namespace BBO.BBO.TeamManagement.UI
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
            Debug.Log("Setting button press");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }
        }
    }
}