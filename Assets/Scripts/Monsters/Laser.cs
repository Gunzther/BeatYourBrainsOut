using UnityEngine;

namespace BBO.BBO.BulletManagement
{
    public class Laser : Bullet
    {
        [SerializeField]
        private LineRenderer lineRenderer = default;

        public LineRenderer LineRenderer => lineRenderer;

        protected override void MoveToTarget()
        {

        }

        private void FixedUpdate()
        {
            MoveToTarget();
        }

        /*        public void SetPosition()
                {

                }*/
    }
}