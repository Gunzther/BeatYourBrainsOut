using BBO.BBO.InterfaceManagement;
using BBO.BBO.GameManagement;
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
        ConnectPlayerManager cpm = nextCanvas.gameObject.GetComponent<ConnectPlayerManager>();
        GameObject temp = cpm?.NextCanvas;
        SetActiveCanvas(temp, currentCanvas);

        GameManager.Instance.ActiveSelectMap = false;
    }

    public void Setting()
    {
        Debug.Log("Setting button press");
    }

    public void Quit()
    {
        Debug.Log("Quit button press");
        Application.Quit();
    }
}
