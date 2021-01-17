using BBO.BBO.PlayerManagement;
using UnityEngine;

namespace BBO.BBO.TeamManagement
{
    public class TeamManager : MonoBehaviour
    {
        [SerializeField]
        private Transform playerPrefab = default;

        [SerializeField]
        private Transform parent = default;

        private Team team = default;

        private void Start()
        {
            team = new Team();

            // TODO: spawn player when any controller is already connected
            SpawnNewPlayer(new Vector3(0, 0, 0));
        }

        public void SpawnNewPlayer(Vector3 position)
        {
            Transform newPlayer = Instantiate(playerPrefab, parent);
            newPlayer.position = position;

            team.AddPlayer(newPlayer.GetComponent<PlayerCharacter>());
        }
    }
}
