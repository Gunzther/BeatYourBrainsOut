using BBO.BBO.InterfaceManagement;
using System.Collections;
using System.Collections.Generic;
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

    public void Setting(){
        Debug.Log("Setting button press");
    }

    public void Quit()
    {
        Debug.Log("Quit button press");
        Application.Quit();
    }
}
