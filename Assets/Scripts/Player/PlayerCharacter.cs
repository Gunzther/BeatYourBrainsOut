using BBO.BBO.TeamManagement;
using BBO.BBO.TeamManagement.UI;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerCharacter : MonoBehaviour
    {
        public PlayerStats CurrentPlayerStats { get; private set; }

        private int playerID = default;
        private UIManager uiManager = default;
        private Team team = default;

        public void SetTeam(Team team)
        {
            this.team = team;
        }

        public void UpdateHpUI()
        {
            uiManager.SetTeamHpValue(team.CurrentTeamHealth);
        }

        private void Awake()
        {
            // TODO: generate player id and assign value to playerID variable
            playerID = 0;
            CurrentPlayerStats = new PlayerStats(playerID);
            uiManager = FindObjectOfType<UIManager>();
        }
    }
}
