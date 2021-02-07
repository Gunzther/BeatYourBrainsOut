using BBO.BBO.GameData;
using BBO.BBO.TeamManagement;
using BBO.BBO.TeamManagement.UI;
using UnityEngine;

namespace BBO.BBO.PlayerManagement
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField]
        private PlayerAnimatorController playerAnimatorController = default;

        public PlayerStats CurrentPlayerStats { get; private set; }

        private int playerID = default;
        private UIManager uiManager = default;
        private Team team = default;

        public void Reload()
        {
            uiManager = FindObjectOfType<UIManager>();
            uiManager.SetTeamHpMaxValue(team.CurrentTeamHealth);
        }

        public void SetTeam(Team team)
        {
            this.team = team;
        }

        public void UpdateHpUI()
        {
            uiManager.SetTeamHpValue(team.CurrentTeamHealth);
        }

        public void TriggerHurtAnimation()
        {
            playerAnimatorController.SetTrigger(PlayerData.HurtTriggerHash);
        }

        private void Awake()
        {
            // TODO: generate player id and assign value to playerID variable
            playerID = 0;
            CurrentPlayerStats = new PlayerStats(playerID);
        }
    }
}
