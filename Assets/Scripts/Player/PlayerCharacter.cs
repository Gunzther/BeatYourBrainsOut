using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerCharacter : MonoBehaviour
    {
        public PlayerStats CurrentPlayerStats { get; private set; }

        private int playerID = default;

        private void Awake()
        {
            // TODO: generate player id and assign value to playerID variable
            playerID = 0;
            CurrentPlayerStats = new PlayerStats(playerID);
        }
    }
}
