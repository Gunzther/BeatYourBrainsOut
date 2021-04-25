using System.Collections;
using System.Collections.Generic;
using BBO.BBO.ScoreSystem;
using UnityEngine;

public class Testing : MonoBehaviour {

    [SerializeField] private UI_StatsRadarChart uiStatsRadarChart;
    private void Start() {
        Scores scores = new Scores(20, 20, 20, 20, 20);
        uiStatsRadarChart.SetStats(scores);
    }

}