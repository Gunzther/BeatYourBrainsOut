using BBO.BBO.GameManagement;
using BBO.BBO.PlayerManagement;
using BBO.BBO.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBO.BBO.TeamManagement
{
    public class TeamManager : MonoSingleton<TeamManager>
    {
        [Header("Prefabs")]
        [SerializeField]
        private Transform playerPrefab = default;

        [SerializeField]
        private Transform parent = default;

        [Header("Spawners")]
        [SerializeField]
        private Transform spawnRingCenter = default;

        [SerializeField]
        private float spawnRingRadius = default;

        //Spawned Players
        private List<PlayerSmoothController> activePlayerControllers = default;
        private PlayerSmoothController focusedPlayerController = default;

        public Team Team => team;
        private Team team = default;
        private int numberOfPlayers = 0;

        public override void Awake()
        {
            base.Awake();
            team = new Team();
            activePlayerControllers = new List<PlayerSmoothController>();
        }

        public void Reload()
        {
            foreach (PlayerCharacter player in team.PlayerCharacters)
            {
                player.Reload();
            }
        }

        public void SetupLocalMultiplayer(InputDevice input)
        {
            if (IsAdded(input))
            {
                return;
            }

            // spawn players
            Transform spawnedPlayer = Instantiate(playerPrefab, parent);
            spawnedPlayer.position = CalculatePositionInRing(numberOfPlayers, activePlayerControllers.Count + 1);

            PlayerSmoothController playerController = spawnedPlayer.GetComponent<PlayerSmoothController>();
            AddPlayerToActivePlayerList(playerController);
            playerController.SetupPlayer(numberOfPlayers, input.deviceId);

            // Add player into the team
            var playerCharacter = spawnedPlayer.GetComponent<PlayerCharacter>();
            playerCharacter.SetTeam(team);
            team.AddPlayer(playerCharacter);

            numberOfPlayers += 1;
        }

        public void RemovePlayer(PlayerSmoothController player)
        {
            GameManager.Instance.RemovePlayer(player.DeviceId);
            activePlayerControllers.Remove(player);
            Destroy(player.gameObject);
        }

        private bool IsAdded(InputDevice input)
        {
            int id = input.deviceId;

            foreach (PlayerSmoothController playerController in activePlayerControllers)
            {
                if (playerController.DeviceId == id)
                {
                    return true;
                }
            }

            return false;
        }

        private void AddPlayerToActivePlayerList(PlayerSmoothController newPlayer)
        {
            activePlayerControllers.Add(newPlayer);
        }

        private void DestroyOldPlayers()
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        //Spawn Utilities
        private Vector3 CalculatePositionInRing(int positionID, int numberOfPlayers)
        {
            if (numberOfPlayers == 1)
            {
                return spawnRingCenter.position;
            }

            float angle = (positionID) * Mathf.PI * 2 / numberOfPlayers;
            float x = Mathf.Cos(angle) * spawnRingRadius;
            float z = Mathf.Sin(angle) * spawnRingRadius;

            return spawnRingCenter.position + new Vector3(x, 0, z);
        }

        //Get Data ----
        public List<PlayerSmoothController> GetActivePlayerControllers()
        {
            return activePlayerControllers;
        }

        public PlayerSmoothController GetFocusedPlayerController()
        {
            return focusedPlayerController;
        }

        public int NumberOfConnectedDevices()
        {
            return InputSystem.devices.Count;
        }
    }
}
