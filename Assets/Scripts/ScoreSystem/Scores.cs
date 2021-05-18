using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BBO.BBO.ScoreSystem
{
    public class Scores
    {

    public event EventHandler OnScoresChanged;

    public static int SCORE_MIN = 0;
    public static int SCORE_MAX = 20;

    public enum Type {
        DamageDealScore,
        DamageReceivedScore,
        CraftingDoneScore,
        JukingDoneScore,
        HealingDoneScore,
    }

    private SingleScore damageDealScore;
    private SingleScore damageReceivedScore;
    private SingleScore craftingDoneScore;
    private SingleScore jukingDoneScore;
    private SingleScore healingDoneScore;

    public Scores(int damageDealScoreAmount, int damageReceivedScoreAmount, int craftingDoneScoreAmount, int jukingDoneScoreAmount, int healingDoneScoreAmount) {
        damageDealScore = new SingleScore(damageDealScoreAmount);
        damageReceivedScore = new SingleScore(damageReceivedScoreAmount);
        craftingDoneScore = new SingleScore(craftingDoneScoreAmount);
        jukingDoneScore = new SingleScore(jukingDoneScoreAmount);
        healingDoneScore = new SingleScore(healingDoneScoreAmount);
    }
    
    private SingleScore GetSingleScore(Type scoreType) {
        switch (scoreType) {
        default:
        case Type.DamageDealScore:       return damageDealScore;
        case Type.DamageReceivedScore:      return damageReceivedScore;
        case Type.CraftingDoneScore:        return craftingDoneScore;
        case Type.JukingDoneScore:         return jukingDoneScore;
        case Type.HealingDoneScore:       return healingDoneScore;
        }
    }
    
    public void SetScoreAmount(Type scoreType, int scoreAmount) {
        GetSingleScore(scoreType).SetScoreAmount(scoreAmount);
        if (OnScoresChanged != null) OnScoresChanged(this, EventArgs.Empty);
    }

    public void IncreaseScoresAmount(Type scoreType) {
        SetScoreAmount(scoreType, GetScoreAmount(scoreType) + 1);
    }

    public void DecreaseScoresAmount(Type scoreType) {
        SetScoreAmount(scoreType, GetScoreAmount(scoreType) - 1);
    }

    public int GetScoreAmount(Type scoreType) {
        return GetSingleScore(scoreType).GetScoreAmount();
    }

    public float GetScoreAmountNormalized(Type scoreType) {
        return GetSingleScore(scoreType).GetScoreAmountNormalized();
    }
    
    /*
     * Represents a Single Score of any Type
     * */
    private class SingleScore {

        private int score;

        public SingleScore(int scoreAmount) {
            SetScoreAmount(scoreAmount);
        }

        public void SetScoreAmount(int scoreAmount) {
            score = Mathf.Clamp(scoreAmount, SCORE_MIN, SCORE_MAX);
        }

        public int GetScoreAmount() {
            return score;
        }

        public float GetScoreAmountNormalized() {
            return (float)score / SCORE_MAX;
        }
    } 
    }
}
