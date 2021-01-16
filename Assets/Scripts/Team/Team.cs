using BBO.BBO.GameData;
using BBO.BBO.PlayerManagement;
using System.Collections.Generic;
using System.Linq;

namespace BBO.BBO.TeamManagement
{
    public class Team
    {
        public int PlayerAmount => playerCharacters.Count();
        public int CurrentTeamHealth => playerCharacters.Select(x => x.CurrentPlayerStats.Health).Sum();
        public int TotalHealth => PlayerAmount * PlayerData.DefaultHealth;

        private HashSet<PlayerCharacter> playerCharacters = default;

        public Team()
        {
            playerCharacters = new HashSet<PlayerCharacter>();
        }

        public void AddPlayer(PlayerCharacter playerCharacter)
        {
            playerCharacters.Add(playerCharacter);
        }
    }
}