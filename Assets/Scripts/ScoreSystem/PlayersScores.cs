using System;
using System.Collections;
using System.Collections.Generic;
using BBO.BBO.PlayerManagement;
using BBO.BBO.ScoreSystem;
using BBO.BBO.TeamManagement;
using BBO.BBO.Utilities;
using UnityEngine;

public class PlayersScores : MonoSingleton<PlayersScores>
{
    [SerializeField] 
    private UI_StatsRadarChart uiStatsRadarChart;

    public event EventHandler OnScoresChanged;

    private float allPlayersSummaryScore;
    private Array playerSummaryScore;
    private PlayerStats playerStats;
    private double upperSD;

    private void Start()
    {
        allPlayersSummaryScore = 0;
    }

    private void Update()
    {
        GetAmountScoresOfEachPlayerScores();
    }
    private void GetAmountScoresOfEachPlayerScores()
    {
        foreach (PlayerCharacter player in TeamManager.Instance.Team.PlayerCharacters)
        {
            Debug.Log(player.CurrentPlayerStats.GetSummaryScore());
            allPlayersSummaryScore += player.CurrentPlayerStats.GetSummaryScore();
            upperSD += Math.Pow(player.CurrentPlayerStats.SummaryScore - CalculateAverage(allPlayersSummaryScore),2);
        }
    }
    private float CalculateAverage(float value)
    {
        float AverageValue = value / TeamManager.Instance.Team.PlayerAmount;
        return AverageValue;
    }

    private Double CalculateSD()
    {
        return Math.Sqrt(upperSD / CalculateAverage(allPlayersSummaryScore));
    }
    
}
