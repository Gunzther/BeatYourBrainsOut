using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameManager : InterfaceManager
{
    private void Update()
    {
        Back();
        CameraTransition();
    }

    public void Setting(){
        Debug.Log("Setting button press");
    }

    public void Quit()
    {
        Debug.Log("Quit button press");
        Application.Quit();
    }
}
