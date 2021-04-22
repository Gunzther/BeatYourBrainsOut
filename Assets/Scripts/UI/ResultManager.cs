using BBO.BBO.InterfaceManagement;
using BBO.BBO.GameManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : InterfaceManager
{
    public override void Next()
    {
        GameManager.Instance.LoadSceneCoroutine("StartGame");
        GameManager.Instance.ActiveSelectMap = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Next();
        }
    }
}
