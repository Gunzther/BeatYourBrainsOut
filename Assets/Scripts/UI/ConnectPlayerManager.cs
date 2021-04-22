using BBO.BBO.InterfaceManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPlayerManager : InterfaceManager
{
    [Header("Previous Page")]
    [SerializeField]
    private GameObject previousCanvas = default;
    [SerializeField]
    private InterfaceManager previousManager = default;
    [SerializeField]
    private Camera previousCamera = default;

    [Header("Next Page")]
    [SerializeField]
    private GameObject nextCanvas = default;
    [SerializeField]
    private Camera nextCamera = default;
    [SerializeField]
    private InterfaceManager nextManager = default;

    public override void Back()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            previousManager.ChangeCameraTransition(previousCamera);
            SetActiveCanvas(previousCanvas, currentCanvas);
        }
    }

    public override void Next()
    {
        SetActiveCanvas(nextCanvas, currentCanvas);
        nextManager.ChangeCameraTransition(nextCamera);
    }

    void Update()
    {
        Back();
        CameraTransition();
    }
}
