using System.Collections.Generic;
using BBO.BBO.PlayerManagement;
using BBO.BBO.TeamManagement;
using BBO.BBO.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    TeamManager teamManager = default;

    //Local Multiplayer
    public GameObject inScenePlayer;
    public GameObject playerPrefab;
    public int numberOfPlayers;

    public Transform spawnRingCenter;
    public float spawnRingRadius;

    //Spawned Players
    private List<PlayerSmoothController> activePlayerControllers;
    private PlayerSmoothController focusedPlayerController;

    // Start is called before the first frame update
    void Start()
    {
        SetupLocalMultiplayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Gameplay");
        }
    }

    void SetupLocalMultiplayer()
    {

        if (inScenePlayer == true)
        {
            Destroy(inScenePlayer);
        }

        activePlayerControllers = new List<PlayerSmoothController>();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            Vector3 spawnPosition = CalculatePositionInRing(i, numberOfPlayers);
            Quaternion spawnRotation = CalculateRotation();

            GameObject spawnedPlayer = Instantiate(playerPrefab, spawnPosition, spawnRotation) as GameObject;
            AddPlayerToActivePlayerList(spawnedPlayer.GetComponent<PlayerSmoothController>());
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
