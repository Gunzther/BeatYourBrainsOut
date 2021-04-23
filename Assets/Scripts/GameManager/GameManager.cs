using System.Collections;
using BBO.BBO.TeamManagement;
using BBO.BBO.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BBO.BBO.GameManager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private TeamManager teamManager = default;
        
        // [SerializeField]
        // private Animator musicAnimator;
        
        private void Start()
        {
            InputDevice inputDevice = InputSystem.devices[0];
            teamManager.SetupLocalMultiplayer(inputDevice);
        }
        
        private void Update()
        {
            teamManager.AddPlayerFromController();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // musicAnimator.SetTrigger("fadeOut");
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
