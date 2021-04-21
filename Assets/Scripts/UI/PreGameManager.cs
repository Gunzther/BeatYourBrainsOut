using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameManager : MonoBehaviour
{
    [Header("Canvas Settings")]
    [SerializeField]
    private GameObject startCanvas = default;

    [Header("Camera Settings")]
    [SerializeField]
    private Camera gameCamera = default;
/*    [SerializeField]
    private Camera connectCamera = default;*/
    [SerializeField]
    private Camera startCamera = default;
    public float cameraSmoothingSpeed = 1;
    
    private bool changeCameraPos = false;
    private Vector3 cameraToPos = default;
    private Quaternion cameraToRotate = default;

    private void Start()
    {
        cameraToPos = gameCamera.transform.position;
        cameraToRotate = gameCamera.transform.rotation;
    }
    public void ChangeCameraTransition(Camera cam)
    {
        cameraToPos = cam.transform.position;
        cameraToRotate = cam.transform.rotation;
        changeCameraPos = true;
    }

    private void Update()
    {
        Vector3 tempPos = gameCamera.transform.position;
        Quaternion tempRotate = gameCamera.transform.rotation;

        if (changeCameraPos && tempPos != cameraToPos)
        {
            if (tempPos == cameraToPos)
            {
                changeCameraPos = false;
            }
            else
            {
                gameCamera.transform.position = Vector3.Lerp(tempPos, cameraToPos, Time.deltaTime * cameraSmoothingSpeed);
                gameCamera.transform.rotation = Quaternion.Lerp(tempRotate, cameraToRotate, Time.deltaTime * cameraSmoothingSpeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeCameraTransition(startCamera);
            startCanvas.SetActive(true);
        }
    }
}
