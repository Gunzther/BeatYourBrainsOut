using System.Collections.Generic;
using UnityEngine;
using System;

namespace BBO.BBO.Utilities
{
    [Serializable]
    public struct CustomInputContextIcon
    {
        public string customInputContextString;
        public Sprite customInputContextIcon;
    }

    [CreateAssetMenu(fileName = "Device Display Settings", menuName = "Scriptable Objects/Device Display Settings", order = 1)]
    public class DeviceDisplaySettings : ScriptableObject
    {
        public string deviceDisplayName = default;
        public Color deviceDisplayColor = default;
        public bool deviceHasContextIcons = default;

        public Sprite buttonNorthIcon = default;
        public Sprite buttonSouthIcon = default;
        public Sprite buttonWestIcon = default;
        public Sprite buttonEastIcon = default;

        public Sprite triggerRightFrontIcon = default;
        public Sprite triggerRightBackIcon = default;
        public Sprite triggerLeftFrontIcon = default;
        public Sprite triggerLeftBackIcon = default;

        public List<CustomInputContextIcon> customContextIcons = new List<CustomInputContextIcon>();
    }
}
