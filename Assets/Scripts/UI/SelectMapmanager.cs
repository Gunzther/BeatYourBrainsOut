using BBO.BBO.GameManagement;
using BBO.BBO.InterfaceManagement;
using BBO.BBO.TeamManagement;
using BBO.BBO.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SelectMapmanager : InterfaceManager
{
    [Header("Previous Page")]
    [SerializeField]
    private GameObject previousCanvas = default;
    [SerializeField]
    private Camera previousCamera = default;

    [Header("Next Page")]
    [SerializeField]
    private Camera nextCamera = default;

    public override void Next()
    {
        mainCamera.transform.position = nextCamera.transform.position;
        mainCamera.transform.rotation = nextCamera.transform.rotation;
        currentCanvas.SetActive(false);
        GameManager.Instance.LoadSceneCoroutine("Gameplay", () => TeamManager.Instance.Reload());
    }

    public override void Back()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainCamera.transform.position = previousCamera.transform.position;
            mainCamera.transform.rotation = previousCamera.transform.rotation;
            SetActiveCanvas(previousCanvas, currentCanvas);
        }
    }

    void Update()
    {
        Back();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.LoadSceneCoroutine("Gameplay", () => TeamManager.Instance.Reload());
        }
    }
}

