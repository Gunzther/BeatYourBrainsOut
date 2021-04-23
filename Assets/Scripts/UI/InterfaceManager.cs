using UnityEngine;

namespace BBO.BBO.InterfaceManagement
{
    public class InterfaceManager : MonoBehaviour
    {
        [Header("Canvas Settings")]
        [SerializeField]
        protected GameObject currentCanvas = default;

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
            Vector3 tempPos = Camera.main.transform.position;
            Quaternion tempRotate = Camera.main.transform.rotation;

            if (changeCameraPos)
            {
                if (tempPos == cameraToPos)
                {
                    changeCameraPos = false;
                }
                else
                {
                    Camera.main.transform.position = Vector3.Lerp(tempPos, cameraToPos, Time.deltaTime * cameraSmoothingSpeed);
                    Camera.main.transform.rotation = Quaternion.Lerp(tempRotate, cameraToRotate, Time.deltaTime * cameraSmoothingSpeed);
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