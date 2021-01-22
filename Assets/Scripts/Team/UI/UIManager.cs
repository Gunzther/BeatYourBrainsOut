﻿using UnityEngine;
using UnityEngine.UI;

namespace BBO.BBO.TeamManagement.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private Slider teamHpSlider;

        [SerializeField]
        private Text teamHpText;

        public void SetTeamHpMaxValue(int value)
        {
            teamHpSlider.maxValue = value;
            teamHpText.text = value.ToString();
        }

        public void SetTeamHpValue(int value)
        {
            if (value < 0)
            {
                value = 0;
            }

            teamHpSlider.value = value;
            teamHpText.text = value.ToString();
        }
    }
}