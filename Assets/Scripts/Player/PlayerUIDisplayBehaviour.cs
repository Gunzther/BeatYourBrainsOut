using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIDisplayBehaviour : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI IDDisplayText = default;

    [SerializeField]
    private TextMeshProUGUI deviceNameDisplayText = default;

    [SerializeField]
    private Image deviceDisplayIcon = default;

    public void UpdatePlayerIDDisplayText(int newPlayerID)
    {
        IDDisplayText.SetText((newPlayerID + 1).ToString());
    }

    public void UpdatePlayerDeviceNameDisplayText(string newDeviceName)
    {
        deviceNameDisplayText.SetText(newDeviceName);
    }

    public void UpdatePlayerIconDisplayColor(Color newColor)
    {
        deviceDisplayIcon.color = newColor;
    }
}