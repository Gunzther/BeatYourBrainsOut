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

        public void ResetResultUI()
        {
            foreach (PlayerResultUI playerUI in playerResultsUI)
            {
                playerUI.SetUIActive(false);
            }
        }

        public void SetPlayerResult(int index, int playerID, int score, PlayerData.PlayerColor color)
        {
            var playerUI = playerResultsUI[index];
            playerUI.SetPlayerName(playerID);
            playerUI.SetPlayerScore(score);
            playerUI.SetPlayerImage(color);
            playerUI.SetUIActive(true);
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
