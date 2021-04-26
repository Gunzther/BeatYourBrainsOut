using BBO.BBO.BulletManagement;
using BBO.BBO.GameData;
using BBO.BBO.PlayerManagement;
using BBO.BBO.TeamManagement;
using System.Collections.Generic;
using UnityEngine;

namespace BBO.BBO.MonsterMovement
{
    public class LegeeMovement : MonstersMovement
    {
        [SerializeField]
        private Rigidbody rb = default;

        [SerializeField]
        private float speed = default;

        [SerializeField]
        private float stop_distance = default;

        [Header("Animation")]
        [SerializeField]
        private Animator legeeAnimator = default;

        [Header("Bullet")]
        [SerializeField]
        private GameObject bullet = default;
        [SerializeField]
        private GameObject bulletSpawnPoint = default;

        private const float bounceForce = 2f;
        private const float bounceAttackedForce = 2.5f;
        private const float targetOffset = 0.05f;

        private IEnumerable<PlayerCharacter> players = default;
        private Transform target = default;
        private float timer = 0f;
        private bool isAttack = false;

        public override void OnAttackMovement()
        {
            base.OnAttackMovement();
            var forceDirection = (target.transform.position - transform.position);

            if (forceDirection.x >= targetOffset || forceDirection.x <= -targetOffset || forceDirection.z >= targetOffset || forceDirection.z <= -targetOffset)
            {
                rb.AddForce(forceDirection * -bounceForce, ForceMode.Impulse);
            }
        }

        public override void OnAttackedMovement()
        {
            base.OnAttackedMovement();
            legeeAnimator.SetTrigger(MonstersData.HurtTriggerHash);
            rb.AddForce(target.transform.position * -bounceAttackedForce, ForceMode.Impulse);
        }

        private void Start()
        {
            TeamManager teamManager = (TeamManager)FindObjectOfType(typeof(TeamManager));
            players = teamManager.Team.PlayerCharacters;
            target = GetClosetPlayer().transform;
        }

        private void FixedUpdate()
        {
            if (isAttack)
            {
                if (timer == 0)
                {
                    Laser();
                }
                else if (timer >= MonstersData.LaserLifeTime)
                {
                    timer = 0;
                    isAttack = false;

                    return;
                }
                timer += Time.deltaTime;
            }
            else
            {
                MoveToTarget();
            }

            if (target.position.x - transform.position.x >= 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        private void Laser()
        {
            GameObject obj = Instantiate(bullet, new Vector3(0, 0, 0), Quaternion.identity);
            Laser tmp = obj.GetComponent<Laser>();
            tmp?.SetLaserTarget(bulletSpawnPoint, GetClosetPlayer());
        }

        private void MoveToTarget()
        {
            float step = speed * Time.deltaTime;
            float moveDistance = Vector3.Distance(transform.position, target.position);
            if (moveDistance > stop_distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            }
            AnimateLegeeMovement(transform.position.x, target.position.x);
        }

        private PlayerCharacter GetClosetPlayer()
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

            return closetPlayer;
        }

        private void AnimateLegeeMovement(float eeeXPos, float targetXPos)
        {
            int triggerHash = MonstersData.WalkSideTriggerHash;
            transform.localScale = new Vector3(1, 1, 1);

            if (Vector3.Distance(transform.position, target.position) <= stop_distance)
            {
                triggerHash = MonstersData.AttackTriggerHash;
                if (target.position.x - transform.position.x >= 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                isAttack = true;
            }
            else if (eeeXPos < targetXPos)
            {
                triggerHash = MonstersData.WalkSideTriggerHash;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (eeeXPos > targetXPos)
            {
                triggerHash = MonstersData.WalkSideTriggerHash;
            }

            if (!legeeAnimator.GetCurrentAnimatorStateInfo(0).IsName(triggerHash.ToString()))
            {
                legeeAnimator.SetTrigger(triggerHash);
            }
        }
    }
}

