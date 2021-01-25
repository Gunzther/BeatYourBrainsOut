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
    public class KikiMovement : MonstersMovement
    {
        [SerializeField]
        private Rigidbody rb = default;

        [SerializeField]
        private float speed = default;


        [Header("Animation")]
        public Animator KikiAnimator = default;

        private const float waitSec = 1;
        private const float bounceForce = 0.2f;

        private IEnumerable<PlayerCharacter> players = default;
        private float timer = default;
        private Transform target = default;

        public override void OnAttackMovement()
        {
            rb.AddForce(target.transform.position * -bounceForce, ForceMode.Impulse);
        }

        private void Start()
        {
            TeamManager teamManager = (TeamManager)FindObjectOfType(typeof(TeamManager));
            players = teamManager.Team.PlayerCharacters;
            timer = 0;
            target = GetClosetPlayer();
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
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            AnimateKikiMovement(transform.position.x, target.position.x);
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

        private void AnimateKikiMovement(float kikiXPos, float targetXPos)
        {
            int triggerHash = MonstersData.IdleTriggerHash;
            transform.localScale = new Vector3(1, 1, 1);

            if (kikiXPos < targetXPos)
            {
                triggerHash = MonstersData.WalkSideTriggerHash;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (kikiXPos > targetXPos)
            {
                triggerHash = MonstersData.WalkSideTriggerHash;
            }

            if (!KikiAnimator.GetCurrentAnimatorStateInfo(0).IsName(triggerHash.ToString()))
            {
                KikiAnimator.SetTrigger(triggerHash);
            }
        }
    }
}
