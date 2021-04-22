using BBO.BBO.GameManagement;
using BBO.BBO.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SelectMapmanager : InterfaceManager
{
    public override void Next()
    {
        mainCamera.transform.position = nextCamera.transform.position;
        currentCanvas.SetActive(false);
        GameManager.Instance.LoadSceneCoroutine("Gameplay");
    }

    void Update()
    {
        Back();
        CameraTransition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.LoadSceneCoroutine("Gameplay");
        }
    }
}

