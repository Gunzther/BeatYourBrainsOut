using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBO.BBO.InterfaceManagement
{
    public class InterfaceManager : MonoBehaviour
    {
        [Header("Canvas Settings")]
        [SerializeField]
        protected GameObject currentCanvas = default;

        [Header("Camera Settings")]
        [SerializeField]
        protected Camera mainCamera = default;

        public void SetChangeCameraPos(bool value) => changeCameraPos = value;
        protected bool changeCameraPos = false;
        protected float cameraSmoothingSpeed = 4;
        protected Vector3 cameraToPos = default;
        protected Quaternion cameraToRotate = default;

        public void ChangeCameraTransition(Camera cam)
        {
            cameraToPos = cam.transform.position;
            cameraToRotate = cam.transform.rotation;
            changeCameraPos = true;
        }

        public void SetActiveCanvas(GameObject activate, GameObject deactivate)
        {
            activate.SetActive(true);
            deactivate.SetActive(false);
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

        }

        public virtual void Next()
        {

        }
    }
}