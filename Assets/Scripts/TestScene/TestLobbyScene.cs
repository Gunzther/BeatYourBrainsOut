using BBO.BBO.GameManagement;
using BBO.BBO.TeamManagement;

using UnityEngine;

public class TestLobbyScene : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.LoadSceneCoroutine("Gameplay", () => TeamManager.Instance.Reload());
        }
    }
}
