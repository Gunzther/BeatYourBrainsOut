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
        private const float offset = 0.001f;

        private IEnumerable<PlayerCharacter> players = default;
        private float timer = default;
        private Transform target = default;

        private void Start()
        {
            TeamManager teamManager = (TeamManager)FindObjectOfType(typeof(TeamManager));
            players = teamManager.Team.PlayerCharacters;
            timer = 0;
            target = GetClosetPlayer();
            Debug.Log(target == null);
        }

        private void Update()
        {
            if (timer > waitSec)
            {
                target = GetClosetPlayer();
                timer = 0;
            }

            MoveToTarget();
            timer += Time.deltaTime;
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
