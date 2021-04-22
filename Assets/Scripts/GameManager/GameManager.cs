using BBO.BBO.TeamManagement;
using BBO.BBO.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBO.BBO.GameManagement
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private TeamManager teamManager = default;

        private void Start()
        {
            InputDevice inputDevice = InputSystem.devices[0];
            teamManager.SetupLocalMultiplayer(inputDevice);
        }

        private void Update()
        {
            teamManager.AddPlayerFromController();
        }

        public void LoadSceneCoroutine(string name)
        {
            StartCoroutine(BBOSceneManager.LoadSceneAsync(name,
                () =>
                {
                    teamManager.Reload();
                    StopAllCoroutines();
                }
                ));
        }
    }
}
