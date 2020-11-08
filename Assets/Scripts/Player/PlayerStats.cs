using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerStats
    {
        public int Health => PlayerPrefs.GetInt(key + playerID);

        private readonly int playerID = default;
        private const string key = "hp";
        private const int defaultValue = 100;

        public PlayerStats(int playerID)
        {
            this.playerID = playerID;
            Clear();
        }

        public void Clear()
        {
            PlayerPrefs.SetInt(key + playerID, defaultValue);
        }

        public void SetPlayerHealth(int value)
        {
            PlayerPrefs.SetInt(key + playerID, value);
        }

        public void DecreasePlayerHealth(int value)
        {
            PlayerPrefs.SetInt(key + playerID, Health - value);
        }

        public void IncreasePlayerHealth(int value)
        {
            PlayerPrefs.SetInt(key + playerID, Health + value);
        }
    }
}
