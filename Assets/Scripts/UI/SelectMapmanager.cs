using BBO.BBO.GameManagement;
using BBO.BBO.TeamManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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




        [Header("Maps")]
        [SerializeField]
        private Image mapSprite = default;
        [SerializeField]
        private Text mapName = default;
        [SerializeField]
        private List<Sprite> allMapSprite = default;
        [SerializeField]
        private List<string> allMapName = default;
        [SerializeField]
        private List<string> sceneName = default;

        private int currentMap = 0;

        public override void Next()
        {
            string tmp = "Gameplay";
            if (currentMap != 0)
            {
                tmp = tmp + currentMap;
            }
            Camera.main.transform.position = nextCamera.transform.position;
            Camera.main.transform.rotation = nextCamera.transform.rotation;
            currentCanvas.SetActive(false);
            GameManager.Instance.LoadSceneCoroutine(tmp, () => TeamManager.Instance.Reload());
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

        public void NextPage()
        {
            currentMap++;
            if (currentMap >= allMapSprite.Count)
            {
                currentMap = 0;
            }
            mapName.text = allMapName[currentMap].ToString();
            mapSprite.sprite = allMapSprite[currentMap];
        }

        public void PreviousPage()
        {
            currentMap--;
            if (currentMap < 0)
            {
                currentMap = allMapSprite.Count - 1;
            }
            mapName.text = allMapName[currentMap].ToString();
            mapSprite.sprite = allMapSprite[currentMap];
        }

        void Update()
        {
            Back();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Next();
            }
        }
    }
}
