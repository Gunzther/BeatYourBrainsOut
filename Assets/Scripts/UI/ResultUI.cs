using BBO.BBO.GameData;
using UnityEngine;
using UnityEngine.UI;

namespace BBO.BBO.UI
{
    public class ResultUI : MonoBehaviour
    {
        [SerializeField]
        private Color starActive = default;

        [SerializeField]
        private Color starNonActive = default;

        [SerializeField]
        private Image[] stars = default;

        [SerializeField]
        private PlayerResultUI[] playerResultsUI = default;

        public void SetPlayerResult(int index, int score, PlayerData.PlayerColor color)
        {
            var playerUI = playerResultsUI[index];
            playerUI.SetPlayerName(index + 1);
            playerUI.SetPlayerScore(score);
            playerUI.SetPlayerImage(color);
        }

        public void SetStars(int activeAmount)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].color = i < activeAmount ? starActive : starNonActive;
            }
        }
    }
}
