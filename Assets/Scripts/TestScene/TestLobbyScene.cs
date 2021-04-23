using BBO.BBO.GameManagement;
using BBO.BBO.TeamManagement;
using UnityEngine;

namespace BBO.BBO.TestScene
{
    public class TestLobbyScene : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.LoadSceneCoroutine("Gameplay", () => TeamManager.Instance.Reload());
            }
        }
    }
}