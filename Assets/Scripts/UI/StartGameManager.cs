using BBO.BBO.GameManagement;
using BBO.BBO.InterfaceManagement;
using UnityEngine;

public class StartGameManager : InterfaceManager
{
    [Header("Next Page")]
    [SerializeField]
    private GameObject nextCanvas = default;

    [SerializeField]
    private Camera nextCamera = default;

    [SerializeField]
    private InterfaceManager nextManager = default;

    [SerializeField]
    private GameObject settingPrefab = default;

    private void Update()
    {
        Back();
        CameraTransition();
    }

    public override void Next()
    {
        SetActiveCanvas(nextCanvas, currentCanvas);
        nextManager.ChangeCameraTransition(nextCamera);
    }

    public void SelectMap()
    {
        if (nextCanvas.gameObject.GetComponent<ConnectPlayerManager>() is ConnectPlayerManager cpm)
        {
            GameObject temp = cpm.NextCanvas;
            SetActiveCanvas(temp, currentCanvas);
        }

        GameManager.Instance.ActiveSelectMap = false;
    }

    public void Setting()
    {
        Instantiate(settingPrefab);
    }

    public void Quit()
    {
        Debug.Log("Quit button press");
        Application.Quit();
    }
}
