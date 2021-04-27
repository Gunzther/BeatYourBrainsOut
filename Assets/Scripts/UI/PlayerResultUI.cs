using BBO.BBO.GameData;
using UnityEngine;
using UnityEngine.UI;

namespace BBO.BBO.UI
{
    public class PlayerResultUI : MonoBehaviour
    {
        [SerializeField]
        private Image playerImage = default;

        [SerializeField]
        private Text playerName = default;

        [SerializeField]
        private Text playerScore = default;

        [SerializeField]
        private Material[] playerColors = default; // white, red, blue, yellow

        public void SetPlayerName(string name)
        {
            playerName.text = name;
        }

        public void SetPlayerName(int number)
        {
            playerName.text = $"Player {number}";
        }

        public void SetPlayerScore(int score)
        {
            if (score < 0) score = 0;
            playerScore.text = score.ToString();
        }

        public void SetPlayerImage(PlayerData.PlayerColor color)
        {
            int colorIndex = 0;

            switch (color)
            {
                case PlayerData.PlayerColor.Red:
                    colorIndex = 1;
                    break;
                case PlayerData.PlayerColor.Blue:
                    colorIndex = 2;
                    break;
                case PlayerData.PlayerColor.Yellow:
                    colorIndex = 3;
                    break;
            }

            playerImage.material = playerColors[colorIndex];
        }

        public void SetUIActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
