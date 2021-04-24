using BBO.BBO.GameData;
using BBO.BBO.TeamManagement;
using BBO.BBO.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBO.BBO.GameManagement
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private int maxPlayerNumber = default;

        public bool ActiveSelectMap
        {
            get => activeSelectMap;
            set => activeSelectMap = value;
        }

        private bool activeSelectMap = false;
        private int currentDevicesCount = 0;
        private HashSet<int> detectedDevices = default;

        public void LoadSceneCoroutine(string name, Action action)
        {
            StartCoroutine(BBOSceneManager.LoadSceneAsync(name,
                () =>
                {
                    action?.Invoke();
                    StopAllCoroutines();
                }
                ));
        }

        public void RemovePlayer(int deviceID)
        {
            detectedDevices.Remove(deviceID);
            currentDevicesCount--;
        }

        private void Start()
        {
            detectedDevices = new HashSet<int>();
            AddNewPlayers();
        }

        private void Update()
        {
            if (activeSelectMap)
            {
                StartGameManager sgm = FindObjectOfType<StartGameManager>();
                sgm?.SelectMap();
            }
        }

        private void FixedUpdate()
        {
            if (InputSystem.devices.Count != detectedDevices.Count)
            {
                print("has new device");
                AddNewPlayers();
            }
        }

        private void AddNewPlayers()
        {
            foreach (InputDevice inputDevice in InputSystem.devices)
            {
                detectedDevices.Add(inputDevice.deviceId);

                if (IsValidController(inputDevice))
                {
                    TeamManager.Instance.SetupLocalMultiplayer(inputDevice);
                    currentDevicesCount++;
                }
            }
        }

        private bool IsValidController(InputDevice inputDevice)
        {
            return !ControllerData.NotGameControllers.Contains(inputDevice.device.name) 
                   && currentDevicesCount < maxPlayerNumber;
        }
    }
}
