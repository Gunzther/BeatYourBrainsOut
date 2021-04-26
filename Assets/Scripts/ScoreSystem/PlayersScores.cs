using System;
using System.Collections;
using System.Collections.Generic;
using BBO.BBO.PlayerManagement;
using BBO.BBO.ScoreSystem;
using BBO.BBO.TeamManagement;
using UnityEngine;

public class PlayersScores : MonoBehaviour
{
    [SerializeField] 
    private UI_StatsRadarChart uiStatsRadarChart;

    public event EventHandler OnScoresChanged;

    private float allPlayersSummaryScore;
    private PlayerStats playerStats;

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
            allPlayersSummaryScore += player.CurrentPlayerStats.GetSummaryScore();
        }
    }
    private float CalculateAverage(float value)
    {
        float AverageValue = value / TeamManager.Instance.Team.PlayerAmount;
        return AverageValue;
    }

    private void CalculateUnityScore()
    {
        
    }
}
