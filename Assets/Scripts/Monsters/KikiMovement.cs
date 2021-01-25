using BBO.BBO.PlayerManagement;
using BBO.BBO.TeamManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BBO.BBO.MonsterMovement
{
    /// <summary>
    /// A movement that monster run into the closet player.
    /// </summary>
    public class KikiMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = default;

        private const float waitSec = 1;
        private const float freezeSec = 6;
        private const float offset = 0.01f;

        private IEnumerable<PlayerCharacter> players = default;
        private float timer = default;
        private float freezeTimer = default;
        private Transform target = default;

        private void Start()
        {
            TeamManager teamManager = (TeamManager)FindObjectOfType(typeof(TeamManager));
            players = teamManager.Team.PlayerCharacters;
            timer = 0;
            freezeTimer = 0;
            target = GetClosetPlayer();
            Debug.Log(target == null);
        }

        private void Update()
        {
            if (freezeTimer > freezeSec)
            {
                target = transform;
                freezeTimer = 0;
                timer = 0;
            }
            else if (timer > waitSec)
            {
                target = GetClosetPlayer();
                timer = 0;
            }

            MoveToTarget();

            timer += Time.deltaTime;
            freezeTimer += Time.deltaTime;
        }

        private void MoveToTarget()
        {
            if (Vector3.Distance(transform.position, target.position) > offset)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
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
}
