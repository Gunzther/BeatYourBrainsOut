using BBO.BBO.PlayerManagement;
using BBO.BBO.Utilities;
using UnityEngine;

namespace BBO.BBO.TeamManagement
{
    public class TeamManager : MonoSingleton<TeamManager>
    {
        [SerializeField]
        private Transform playerPrefab = default;

        [SerializeField]
        private Transform parent = default;

        public Team Team => team;
        private Team team = default;

        public void SpawnNewPlayer(Vector3 position)
        {
            Transform newPlayer = Instantiate(playerPrefab, parent);
            newPlayer.position = position;

            var playerCharacter = newPlayer.GetComponent<PlayerCharacter>();
            playerCharacter.SetTeam(team);
            team.AddPlayer(playerCharacter);
        }

        private void Start()
        {
            team = new Team();

            // TODO: spawn player when any controller is already connected
            SpawnNewPlayer(new Vector3(0, 0, 0));
        }
    }
}
