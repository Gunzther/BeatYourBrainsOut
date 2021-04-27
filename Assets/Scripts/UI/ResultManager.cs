using BBO.BBO.GameData;
using BBO.BBO.GameManagement;
using BBO.BBO.InterfaceManagement;
using BBO.BBO.PlayerManagement;
using BBO.BBO.TeamManagement;
using BBO.BBO.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResultManager : InterfaceManager
{
    [SerializeField]
    private ResultUI resultUI = default;

    private List<int> summaryScores = default;

    private int[] dealDamageScores = default;
    private int[] receiveDamageScores = default;
    private int[] craftScores = default;

    private int[] dealDamagePayOffs = default;
    private int[] receiveDamagePayOffs = default;
    private int[] craftPayOffs = default;

    private int[,] dealDamageShapleyScores = default;
    private int[,] receiveDamageShapleyScores = default;
    private int[,] craftShapleyScores = default;

    private readonly int[,] pattern = { 
        { 0, -1, -1, -1 },
        { 1, -1, -1, -1},
        { 2, -1, -1, -1},
        { 3, -1, -1, -1},
        { 0, 1, -1, -1 },
        { 0, 2, -1, -1},
        { 0, 3, -1, -1},
        { 1, 2, -1, -1},
        { 1, 3, -1, -1},
        { 2, 3, -1, -1},
        { 0, 1, 2, -1},
        { 0, 1, 3, -1},
        { 0, 2, 3, -1},
        { 1, 2, 3, -1},
        { 0, 1, 2, 3 },
    };

    private readonly int[] set1 = { 0 };
    private readonly int[] set2 = { 0, 1, 4 };
    private readonly int[] set3 = { 0, 1, 2, 4, 5, 7, 10 };
    private readonly int[] set4 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    private readonly (int, int)[,] shapleyPattern1 = { { (0, 0) } };
    private readonly (int, int)[,] shapleyPattern2 = { { (0, 0), (2, 1) }, { (1, 1), (2, 0) } };
    private readonly (int, int)[,] shapleyPattern3 = { 
        { (0, 0), (3, 1), (6, 2) }, 
        { (0, 0), (4, 2), (6, 1) }, 
        { (1, 1), (3, 0), (6, 2) }, 
        { (1, 1), (5, 2), (6, 0)}, 
        { (2, 2), (4, 0), (6, 1) }, 
        { (2, 2), (5, 1), (6, 0)} 
    };

    public override void Next()
    {
        GameManager.Instance.LoadSceneCoroutine("StartGame", null);
        GameManager.Instance.ActiveSelectMap = true;
    }

    private void Start()
    {
        int index = 0;
        summaryScores = new List<int>();
        resultUI.ResetResultUI();

        foreach (PlayerCharacter player in TeamManager.Instance.Team.PlayerCharacters)
        {
            var stat = player.CurrentPlayerStats;
            resultUI.SetPlayerResult(index, player.PlayerID, stat.SummaryScore, PlayerData.PlayerColor.Red);
            summaryScores.Add(stat.SummaryScore);
            print($"[{nameof(ResultManager)}] damage: {stat.DamageDealScore}, re damage: {stat.DamageReceivedScore}, craft: {stat.CraftingDoneScore}");

            index++;
        }

        float teamWorkPercentage = GetSimpleTeamWorkPercentage();
        resultUI.SetTeamWorkPercentage(teamWorkPercentage);
        resultUI.SetStars(GetStarAmount(teamWorkPercentage)); // TODO: set stars depends on Shapley formula
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Next();
        }
    }

    private float GetSimpleTeamWorkPercentage()
    {
        if (summaryScores.Count <= 1)
        {
            return 100;
        }

        summaryScores.Sort();
        List<float> diff = new List<float>();

        for (int i = 1; i < summaryScores.Count; i++)
        {
            int prev = summaryScores[i - 1] < 0 ? 0 : summaryScores[i - 1];
            int current = summaryScores[i] < 0 ? 0 : summaryScores[i];

            diff.Add(current - prev);
        }

        float diffSum = (float)diff.Average();
        float maxDiff = summaryScores.Last();
        float diffPercentage = (diffSum * 100) / maxDiff;

        return 100 - diffPercentage;
    }

    private int GetStarAmount(float teamWorkPercentage)
    {
        if (teamWorkPercentage > 50f)
        {
            int sumScore = summaryScores.Sum();

            if (sumScore < 50) return 0;
            else if (sumScore < 100) return 1;
            else if (sumScore < 150) return 2;
            else return 3;
        }
        else if (teamWorkPercentage <= 10f)
        {
            return 0;
        }
        else if (teamWorkPercentage <= 35f)
        {
            return 1;
        }
        else if (teamWorkPercentage < 70f)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    private void CalculateShapleyValue()
    {
        CreateTotalPayOffs();

        if (TeamManager.Instance.Team.PlayerAmount is int playerAmount && playerAmount < 4)
        {
            (int getIndex, int setIndex)[,] usedPattern = shapleyPattern1;

            switch (playerAmount)
            {
                case 2:
                    usedPattern = shapleyPattern2;
                    break;
                case 3:
                    usedPattern = shapleyPattern3;
                    break;
            }

            dealDamageShapleyScores = new int[playerAmount, usedPattern.Length];
            receiveDamageShapleyScores = new int[playerAmount, usedPattern.Length];
            craftShapleyScores = new int[playerAmount, usedPattern.Length];

            for (int i = 0; i < usedPattern.Length; i++)
            {
                for (int j = 0; j < playerAmount; j++)
                {
                    (int getIndex, int setIndex) value = usedPattern[i, j];
                    int preVal = 0;

                    if (j == 0)
                    {
                        dealDamageShapleyScores[value.setIndex, i] = dealDamagePayOffs[value.getIndex];
                        preVal = dealDamagePayOffs[value.getIndex];
                    }
                    else
                    {
                        dealDamageShapleyScores[value.setIndex, i] = dealDamagePayOffs[value.getIndex] - preVal;
                    }
                }
            }
        }
        else
        {

        }

    }

    private void CreateTotalPayOffs()
    {
        AssignPlayersScore();

        int[] usedSet = set1;

        switch (TeamManager.Instance.Team.PlayerAmount)
        {
            case 2:
                usedSet = set2;
                break;
            case 3:
                usedSet = set3;
                break;
            case 4:
                usedSet = set4;
                break;
        }

        dealDamagePayOffs = new int[usedSet.Length];
        receiveDamagePayOffs = new int[usedSet.Length];
        craftPayOffs = new int[usedSet.Length];

        for (int i = 0; i < usedSet.Length; i++)
        {
            int dealDamagePayOff = 1;
            int receiveDamagePayOff = 1;
            int craftPayOff = 1;

            for (int j = 0; j < 4; j++)
            {
                int index = pattern[i, j];

                if (index == -1) break;

                dealDamagePayOff *= dealDamageScores[index];
                receiveDamagePayOff *= receiveDamagePayOffs[index];
                craftPayOff *= craftPayOffs[index];
            }

            dealDamagePayOffs[i] = dealDamagePayOff;
            receiveDamagePayOffs[i] = receiveDamagePayOff;
            craftPayOffs[i] = craftPayOff;
        }
    }

    private void AssignPlayersScore()
    {
        int amount = TeamManager.Instance.Team.PlayerAmount;

        dealDamageScores = new int[amount];
        receiveDamageScores = new int[amount];
        craftScores = new int[amount];

        int i = 0;

        foreach (PlayerCharacter player in TeamManager.Instance.Team.PlayerCharacters)
        {
            PlayerStats stat = player.CurrentPlayerStats;
            dealDamageScores[i] = stat.DamageDealScore;
            receiveDamageScores[i] = stat.DamageReceivedScore;
            craftScores[i] = stat.CraftingDoneScore;
        }
    }
}
