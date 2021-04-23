using System;
using System.Collections.Generic;
using BBO.BBO.PlayerManagement;
using UnityEngine;

    public class Bullet : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = default;
    private Vector3 target = default;
    public Vector3 Target
    {
        get => target;
        set => target = value;
    }

    private void FixedUpdate()
    {
        MoveToTarget();
    }
    
    private void MoveToTarget()
    {
        Vector3 currentPos = transform.position;

        if (currentPos == target)
        {
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);

    }
}