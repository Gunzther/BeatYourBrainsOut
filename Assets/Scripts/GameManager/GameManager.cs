using BBO.BBO.TeamManagement;
using BBO.BBO.Utilities;
using UnityEngine;

namespace BBO.BBO.GameManager
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        TeamManager teamManager = default;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(BBOSceneManager.LoadSceneAsync("Gameplay", 
                    () => {
                        teamManager.Reload();
                        StopAllCoroutines();
                        }
                    ));
            }
        }
    }
}
