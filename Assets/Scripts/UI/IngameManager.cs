using BBO.BBO.InterfaceManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : InterfaceManager
{
    public override void Back()
    {
        Setting();
    }

    public void Setting()
    {
        Debug.Log("Setting button press");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }
}
