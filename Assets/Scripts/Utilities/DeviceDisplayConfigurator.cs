using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBO.BBO.Utilities
{
    [CreateAssetMenu(fileName = "Device Display Configurator", menuName = "Scriptable Objects/Device Display Configurator", order = 1)]
    public class DeviceDisplayConfigurator : ScriptableObject
    {
        [Serializable]
        public struct DeviceSet
        {
            public string deviceRawPath;
            public DeviceDisplaySettings deviceDisplaySettings;
        }

        [Serializable]
        public struct DisconnectedSettings
        {
            public string disconnectedDisplayName;
            public Color disconnectedDisplayColor;
        }

        public HashSet<DeviceSet> listDeviceSets = new HashSet<DeviceSet>();
        public DisconnectedSettings disconnectedDeviceSettings = default;

        private Color fallbackDisplayColor = Color.white;

        public string GetDeviceName(PlayerInput playerInput)
        {
            string currentDeviceRawPath = playerInput.devices[0].ToString();
            string newDisplayName = null;

            foreach (DeviceSet device in listDeviceSets)
            {
                if (device.deviceRawPath == currentDeviceRawPath)
                {
                    newDisplayName = device.deviceDisplaySettings.deviceDisplayName;
                }
            }

            if (newDisplayName == null)
            {
                newDisplayName = currentDeviceRawPath;
            }

            return newDisplayName;
        }


        public Color GetDeviceColor(PlayerInput playerInput)
        {
            string currentDeviceRawPath = playerInput.devices[0].ToString();

            Color newDisplayColor = fallbackDisplayColor;

            foreach (DeviceSet device in listDeviceSets)
            {
                if (device.deviceRawPath == currentDeviceRawPath)
                {
                    newDisplayColor = device.deviceDisplaySettings.deviceDisplayColor;
                }
            }

            return newDisplayColor;
        }

        public Sprite GetDeviceBindingIcon(PlayerInput playerInput, string playerInputDeviceInputBinding)
        {
            string currentDeviceRawPath = playerInput.devices[0].ToString();

            Sprite displaySpriteIcon = null;

            foreach (DeviceSet device in listDeviceSets)
            {
                if (device.deviceRawPath == currentDeviceRawPath)
                {
                    if (device.deviceDisplaySettings.deviceHasContextIcons != null)
                    {
                        displaySpriteIcon = FilterForDeviceInputBinding(device, playerInputDeviceInputBinding);
                    }
                }
            }

            return displaySpriteIcon;
        }

        private Sprite FilterForDeviceInputBinding(DeviceSet targetDeviceSet, string inputBinding)
        {
            Sprite spriteIcon = null;

            switch (inputBinding)
            {
                case "Button North":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.buttonNorthIcon;
                    break;

                case "Button South":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.buttonSouthIcon;
                    break;

                case "Button West":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.buttonWestIcon;
                    break;

                case "Button East":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.buttonEastIcon;
                    break;

                case "Right Shoulder":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerRightFrontIcon;
                    break;

                case "Right Trigger":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerRightBackIcon;
                    break;

                case "rightTriggerButton":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerRightBackIcon;
                    break;

                case "Left Shoulder":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerLeftFrontIcon;
                    break;

                case "Left Trigger":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerLeftBackIcon;
                    break;

                case "leftTriggerButton":
                    spriteIcon = targetDeviceSet.deviceDisplaySettings.triggerLeftBackIcon;
                    break;

                default:
                    for (int i = 0; i < targetDeviceSet.deviceDisplaySettings.customContextIcons.Count; i++)
                    {
                        var customContextIcon = targetDeviceSet.deviceDisplaySettings.customContextIcons[i];

                        if (customContextIcon.customInputContextString == inputBinding)
                        {
                            if (customContextIcon.customInputContextIcon != null)
                            {
                                spriteIcon = customContextIcon.customInputContextIcon;
                            }
                        }
                    }

                    break;
            }

            return spriteIcon;
        }

        public string GetDisconnectedName()
        {
            return disconnectedDeviceSettings.disconnectedDisplayName;
        }

        public Color GetDisconnectedColor()
        {
            return disconnectedDeviceSettings.disconnectedDisplayColor;
        }
    }
}
