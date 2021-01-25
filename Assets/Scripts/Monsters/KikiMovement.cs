using BBO.BBO.GameData;
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

        [Header("Animation")]
        public Animator KikiAnimator = default;

        private const float waitSec = 1;
        private const float offset = 0.1f;

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

            timer += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        private void MoveToTarget()
        {
            if (Vector3.Distance(transform.position, target.position) > offset is bool isWalking)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }

            AnimateKikiMovement(isWalking, transform.position.x, target.position.x);
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

        private void AnimateKikiMovement(bool isWalking, float kikiXPos, float targetXPos)
        {
            int triggerHash = AnimationTriggerData.IdleTriggerHash;
            transform.localScale = new Vector3(1, 1, 1);

            if (isWalking)
            {
                if (kikiXPos < targetXPos)
                {
                    triggerHash = AnimationTriggerData.WalkSideTriggerHash;
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    triggerHash = AnimationTriggerData.WalkSideTriggerHash;
                }
            }

            if (!KikiAnimator.GetCurrentAnimatorStateInfo(0).IsName(triggerHash.ToString()))
            {
                KikiAnimator.SetTrigger(triggerHash);
            }
        }
    }
}
