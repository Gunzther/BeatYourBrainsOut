using System.Collections.Generic;
using BBO.BBO.PlayerManagement;
using BBO.BBO.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBO.BBO.TeamManagement
{
    public class TeamManager : MonoSingleton<TeamManager>
    {
        [SerializeField]
        private Transform playerPrefab = default;

        [SerializeField]
        private Transform parent = default;

        private Team team = default;

        public Transform spawnRingCenter;
        public float spawnRingRadius;

        //Local Multiplayer
        public GameObject inScenePlayer;
        public int numberOfPlayers;


        //Spawned Players
        private List<PlayerSmoothController> activePlayerControllers;
        private PlayerSmoothController focusedPlayerController;

        private void Start()
        {
            team = new Team();

            SetupLocalMultiplayer();
        }

        public void SetupLocalMultiplayer()
        {

            if (inScenePlayer == true)
            {
                Destroy(inScenePlayer);
            }

            activePlayerControllers = new List<PlayerSmoothController>();

            for (int i = 0; i < numberOfPlayers; i++)
            {
                // spawn players
                Vector3 spawnPosition = CalculatePositionInRing(i, numberOfPlayers);
                Quaternion spawnRotation = CalculateRotation();

                Transform spawnedPlayer = Instantiate(playerPrefab, spawnPosition, spawnRotation);
                AddPlayerToActivePlayerList(spawnedPlayer.GetComponent<PlayerSmoothController>());

                // Add player into the team
                var playerCharacter = spawnedPlayer.GetComponent<PlayerCharacter>();
                playerCharacter.SetTeam(team);
                team.AddPlayer(playerCharacter);
            }

            SetupActivePlayers();
        }

        void SetupActivePlayers()
        {
            for (int i = 0; i < activePlayerControllers.Count; i++)
            {
                activePlayerControllers[i].SetupPlayer(i);
            }
        }

        void AddPlayerToActivePlayerList(PlayerSmoothController newPlayer)
        {
            activePlayerControllers.Add(newPlayer);
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

        //Spawn Utilities

        Vector3 CalculatePositionInRing(int positionID, int numberOfPlayers)
        {
            if (numberOfPlayers == 1)
                return spawnRingCenter.position;

            float angle = (positionID) * Mathf.PI * 2 / numberOfPlayers;
            float x = Mathf.Cos(angle) * spawnRingRadius;
            float z = Mathf.Sin(angle) * spawnRingRadius;
            return spawnRingCenter.position + new Vector3(x, 0, z);
        }

        Quaternion CalculateRotation()
        {
            return Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        }

    }
}
