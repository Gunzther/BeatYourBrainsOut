using System.Collections;
using System.Collections.Generic;
using BBO.BBO.PlayerManagement;
using BBO.BBO.TeamManagement;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private void GetScore()
    {
        foreach (PlayerCharacter player in TeamManager.Instance.Team.PlayerCharacters)
        {
            // TODO : Add logic for calculate scores for each players
        }
    }
}
