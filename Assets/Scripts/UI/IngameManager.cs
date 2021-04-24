using BBO.BBO.InterfaceManagement;
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
