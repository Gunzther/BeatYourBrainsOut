using System.Collections;
using System.Collections.Generic;
using BBO.BBO.PlayerManagement;
using BBO.BBO.TeamManagement;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int damageDealScore = default;
    private int damageReceivedScore = default;
    private int craftingDoneScore = default;
    private int jukingDoneScore = default;
    private int healingDoneScore = default;
    private void GetScore()
    {
        foreach (PlayerCharacter player in TeamManager.Instance.Team.PlayerCharacters)
        {
           
        }
    }
}
