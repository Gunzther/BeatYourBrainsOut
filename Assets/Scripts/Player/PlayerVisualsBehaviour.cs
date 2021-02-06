using UnityEngine;
using UnityEngine.InputSystem;
using BBO.BBO.Utilities;

namespace BBO.BBO.PlayerInputSystem
{
    public class PlayerVisualsBehaviour : MonoBehaviour
    {
        //Player ID
        private int playerID = default;
        private PlayerInput playerInput = default;

        [SerializeField]
        private DeviceDisplayConfigurator deviceDisplaySettings;

        [SerializeField]
        private PlayerUIDisplayBehaviour playerUIDisplayBehaviour;

        public void SetupBehaviour(int newPlayerID, PlayerInput newPlayerInput)
        {
            playerID = newPlayerID;
            playerInput = newPlayerInput;

            UpdatePlayerVisuals();
        }

        public void UpdatePlayerVisuals()
        {
            UpdateUIDisplay();
        }

        private void UpdateUIDisplay()
        {
            playerUIDisplayBehaviour.UpdatePlayerIDDisplayText(playerID);

            string deviceName = deviceDisplaySettings.GetDeviceName(playerInput);
            playerUIDisplayBehaviour.UpdatePlayerDeviceNameDisplayText(deviceName);

            //Color deviceColor = deviceDisplaySettings.GetDeviceColor(playerInput);
            //playerUIDisplayBehaviour.UpdatePlayerIconDisplayColor(deviceColor);
        }

        public void SetDisconnectedDeviceVisuals()
        {
            string disconnectedName = deviceDisplaySettings.GetDisconnectedName();
            playerUIDisplayBehaviour.UpdatePlayerDeviceNameDisplayText(disconnectedName);

            //Color disconnectedColor = deviceDisplaySettings.GetDisconnectedColor();
            //playerUIDisplayBehaviour.UpdatePlayerIconDisplayColor(disconnectedColor);
        }
    }
}
