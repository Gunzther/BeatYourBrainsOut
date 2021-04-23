using BBO.BBO.InterfaceManagement;
using BBO.BBO.GameManagement;
using UnityEngine;

public class ResultManager : InterfaceManager
{
    public override void Next()
    {
        GameManager.Instance.LoadSceneCoroutine("StartGame", null);
        GameManager.Instance.ActiveSelectMap = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Next();
        }
    }
}
