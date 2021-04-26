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
    
    private int amountDamageDealScore;
    private int amountDamageReceivedScore;
    private int amountCraftingDoneScore;
    private int amountJuckingDonScore;
    private int amountHealingDoneScore;
    private PlayerStats playerStats;

    private void Start()
    {
        amountDamageDealScore = 0;
        amountDamageReceivedScore = 0;
        amountCraftingDoneScore = 0;
        amountJuckingDonScore = 0;
        amountHealingDoneScore = 0;
    }

    private void Update()
    {
        GetAmountScoresOfEachPlayerScores();
    }
    private void GetAmountScoresOfEachPlayerScores()
    {
        foreach (PlayerCharacter player in TeamManager.Instance.Team.PlayerCharacters)
        {
            amountDamageDealScore += player.CurrentPlayerStats.DamageDealScore;
            amountDamageReceivedScore += player.CurrentPlayerStats.DamageReceivedScore;
            amountCraftingDoneScore += player.CurrentPlayerStats.CraftingDoneScore;
            amountJuckingDonScore += player.CurrentPlayerStats.JukingDoneScore;
            amountHealingDoneScore += player.CurrentPlayerStats.HealingDoneScore;
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
