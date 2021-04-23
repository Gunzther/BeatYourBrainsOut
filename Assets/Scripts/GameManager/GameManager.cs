using BBO.BBO.TeamManagement;
using BBO.BBO.Utilities;
using System;
using UnityEngine.InputSystem;

namespace BBO.BBO.GameManagement
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private bool activeSelectMap = false;
        public bool ActiveSelectMap
        {
            get => activeSelectMap;
            set => activeSelectMap = value;
        }

        private void Start()
        {
            InputDevice inputDevice = InputSystem.devices[0];
            TeamManager.Instance.SetupLocalMultiplayer(inputDevice);
        }

        private void Update()
        {
            if (activeSelectMap)
            {
                TeamManager.Instance.AddPlayerFromController();
                StartGameManager sgm = FindObjectOfType<StartGameManager>();
                sgm?.SelectMap();
            }
        }

        public void LoadSceneCoroutine(string name, Action action)
        {
            StartCoroutine(BBOSceneManager.LoadSceneAsync(name,
                () =>
                {
                    action?.Invoke();
                    StopAllCoroutines();
                }
            ));
        }
    }
}