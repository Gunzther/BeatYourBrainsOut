using BBO.BBO.PlayerManagement;
using UnityEngine;

namespace BBO.BBO.TeamManagement
{
    public class TeamManager : MonoBehaviour
    {
        [SerializeField]
        private Transform playerPrefab;

        [SerializeField]
        private Transform parent;

        private Team team = default;

        private void Start()
        {
            team = new Team();
        }

        public void SpawnNewPlayer(Vector3 position)
        {
            Transform newPlayer = Instantiate(playerPrefab, parent);
            newPlayer.position = position;

            team.AddPlayer(newPlayer.GetComponent<PlayerCharacter>());
        }
    }
}
