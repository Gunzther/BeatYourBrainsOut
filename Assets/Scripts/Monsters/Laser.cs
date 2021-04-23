using BBO.BBO.GameData;
using BBO.BBO.PlayerManagement;
using UnityEngine;

namespace BBO.BBO.BulletManagement
{
    public class Laser : Bullet
    {
        [SerializeField]
        private LineRenderer lineRenderer = default;
        private float timer = 0;
        private GameObject startPosition = default;
        private PlayerCharacter playerTarget = default;
        public void SetLaserTarget(GameObject start, PlayerCharacter target)
        {
            startPosition = start;
            playerTarget = target;
        }

        protected override void MoveToTarget()
        {
            if (!lineRenderer.enabled) lineRenderer.enabled = true;
            Vector3 s = startPosition.transform.position;
            Vector3 p = playerTarget.transform.position;
            lineRenderer.SetPosition(0, new Vector3(s.x, s.y, s.z));
            lineRenderer.SetPosition(1, new Vector3(p.x, p.y + 0.5f, p.z));
        }

        private void FixedUpdate()
        {
            MoveToTarget();
            timer += Time.deltaTime;

            if (timer >= MonstersData.LaserLifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}