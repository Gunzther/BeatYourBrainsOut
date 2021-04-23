using BBO.BBO.GameManagement;
using BBO.BBO.TeamManagement;
using UnityEngine;

namespace BBO.BBO.InterfaceManagement
{
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
            Camera.main.transform.position = nextCamera.transform.position;
            Camera.main.transform.rotation = nextCamera.transform.rotation;
            currentCanvas.SetActive(false);
            GameManager.Instance.LoadSceneCoroutine("Gameplay", () => TeamManager.Instance.Reload());
        }

        public override void Back()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Camera.main.transform.position = previousCamera.transform.position;
                Camera.main.transform.rotation = previousCamera.transform.rotation;
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
}
