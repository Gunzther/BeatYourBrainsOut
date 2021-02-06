using BBO.BBO.TeamManagement;
using BBO.BBO.Utilities;
using UnityEngine;

namespace BBO.BBO.GameManager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        TeamManager teamManager = default;

        private void Start()
        {
            teamManager.SetupLocalMultiplayer();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(BBOSceneManager.LoadSceneAsync("Gameplay",
                    () =>
                    {
                        teamManager.Reload();
                        StopAllCoroutines();
                    }
                    ));
            }
        }
    }
}
