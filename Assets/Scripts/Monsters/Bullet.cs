using System;
using System.Collections.Generic;
using BBO.BBO.PlayerManagement;
using UnityEngine;

    public class Bullet : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = default;
    
    private IEnumerable<PlayerCharacter> players = default;
    private Transform target = default;

    private void Start()
    {
        target = GetClosetPlayer();
    }
    
    private void FixedUpdate()
    {
        MoveToTarget();
    }
    
    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed);
    }
    
    private Transform GetClosetPlayer()
    {
        PlayerCharacter closetPlayer = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (PlayerCharacter player in players)
        {
            float dist = Vector3.Distance(player.transform.position, currentPos);

            if (dist < minDist)
            {
                closetPlayer = player;
                minDist = dist;
            }
        }

        return closetPlayer.transform;
    }
}