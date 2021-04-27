using BBO.BBO.GameData;
using BBO.BBO.GameManagement;
using BBO.BBO.InterfaceManagement;
using BBO.BBO.PlayerManagement;
using BBO.BBO.TeamManagement;
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
        int index = 0;
        resultUI.ResetResultUI();

        foreach (PlayerCharacter player in TeamManager.Instance.Team.PlayerCharacters)
        {
            var stat = player.CurrentPlayerStats;
            resultUI.SetPlayerResult(index, player.PlayerID, stat.SummaryScore, PlayerData.PlayerColor.Red);
            resultUI.SetStars(3); // TODO: set stars depends on Shapley formula
            print($"[{nameof(ResultManager)}] damage: {stat.DamageDealScore}, re damage: {stat.DamageReceivedScore}, craft: {stat.CraftingDoneScore}");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Next();
        }
    }
}
