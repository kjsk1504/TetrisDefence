using TetrisDefence.Game.Enemy;
using Unity.Collections;
using UnityEngine;

namespace TetrisDefence.Game
{
    public class Bullet : PoolBase
    {
        [ReadOnly, SerializeField] private Vector3 translate = default;
        [ReadOnly, SerializeField] private float angleRad = default;
        public float bulletSpeed = default;
        public float bulletDamage = default;


        public override void Born()
        {
            base.Born();

            angleRad = Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 90);
            translate = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
        }

        public override void Death()
        {
            base.Death();

            angleRad = default;
            translate = Vector3.zero;
        }

        private void FixedUpdate()
        {
            transform.position += translate * bulletSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyBase enemy))
            {
                enemy.Death();
                Death();
            }
            else
            {
                throw new System.Exception($"[Bullet]: EnemyBase가 아닌 물체와 트리거 충돌");
            }
        }
    }
}
