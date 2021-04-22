using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    [Header("Canvas Settings")]
    [SerializeField]
    protected GameObject currentCanvas = default;
    [SerializeField]
    protected GameObject previousCanvas = default;
    [SerializeField]
    protected GameObject nextCanvas = default;
    [Header("Camera Settings")]
    [SerializeField]
    protected Camera mainCamera = default;
    [SerializeField]
    protected Camera previousCamera = default;
    [SerializeField]
    protected Camera nextCamera = default;
    [SerializeField]
    protected InterfaceManager previousManager = default;
    [SerializeField]
    protected InterfaceManager nextManager = default;

    protected float cameraSmoothingSpeed = 4;

    public void SetChangeCameraPos(bool value) => changeCameraPos = value;
    protected bool changeCameraPos = false;
    protected Vector3 cameraToPos = default;
    protected Quaternion cameraToRotate = default;

    public void ChangeCameraTransition(Camera cam)
    {
        cameraToPos = cam.transform.position;
        cameraToRotate = cam.transform.rotation;
        changeCameraPos = true;
    }

    public void CameraTransition()
    {
        Vector3 tempPos = mainCamera.transform.position;
        Quaternion tempRotate = mainCamera.transform.rotation;

        if (changeCameraPos && tempPos != cameraToPos)
        {
            if (tempPos == cameraToPos)
            {
                changeCameraPos = false;
            }
            else
            {
                mainCamera.transform.position = Vector3.Lerp(tempPos, cameraToPos, Time.deltaTime * cameraSmoothingSpeed);
                mainCamera.transform.rotation = Quaternion.Lerp(tempRotate, cameraToRotate, Time.deltaTime * cameraSmoothingSpeed);
            }
        }
    }

    public virtual void Back()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            previousManager.ChangeCameraTransition(previousCamera);
            currentCanvas.SetActive(false);
            previousCanvas.SetActive(true);
        }
    }

    public virtual void Next()
    {
        currentCanvas.SetActive(false);
        nextCanvas.SetActive(true);
        nextManager.ChangeCameraTransition(nextCamera);
    }
}
