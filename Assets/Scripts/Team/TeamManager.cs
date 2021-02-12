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
        private int numberOfPlayers = 1;

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

        public void SetupLocalMultiplayer()
        {
            // spawn players
            Transform spawnedPlayer = Instantiate(playerPrefab, parent);
            spawnedPlayer.position = CalculatePositionInRing(numberOfPlayers, activePlayerControllers.Count+1);
            AddPlayerToActivePlayerList(spawnedPlayer.GetComponent<PlayerSmoothController>());

            // Add player into the team
            var playerCharacter = spawnedPlayer.GetComponent<PlayerCharacter>();
            playerCharacter.SetTeam(team);
            team.AddPlayer(playerCharacter);

            numberOfPlayers += 1;
            SetupActivePlayers();
        }

        private void SetupActivePlayers()
        {

            for (int i = 0; i < activePlayerControllers.Count; i++)
            {
                //activePlayerControllers[i].SetupPlayer(i);
                //InputDevice.deviceId;
            }
        }

        public void AddPlayerFromController()
        {
           if (Gamepad.current.selectButton.wasReleasedThisFrame)
           {
                SetupLocalMultiplayer();
           }
        }

        private void Start()
        {
            SetupActivePlayers();
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
