using BBO.BBO.GameManagement;
using BBO.BBO.InterfaceManagement;
using BBO.BBO.UI;
using UnityEngine;

public class ResultManager : InterfaceManager
{
    [SerializeField]
    private ResultUI resultUI = default;

    public override void Next()
    {
        GameManager.Instance.LoadSceneCoroutine("StartGame", null);
        GameManager.Instance.ActiveSelectMap = true;
    }

    private void Start()
    {
        // TODO: Calculate score &  Update UI
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Next();
        }
    }
}
