using System;
using System.Threading;
using Unity.Collections;
using UnityEngine;

namespace TetrisDefence.Game.Pool
{
    public class Bullet : ItemBase
    {
        [ReadOnly, SerializeField] private Vector3 translate = default;
        [ReadOnly, SerializeField] private float angleRad = default;
        private float _bulletSpeed = default;
        private float _bulletDamage = default;
        private float _bulletLifeTime = default;
        private float _dotTime = default;
        private float _slowTime = default;


        public override void Born()
        {
            base.Born();

            angleRad = Mathf.Deg2Rad * (transform.rotation.eulerAngles.z + 90);
            translate = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad), 0);
            
            Invoke(nameof(Death), _bulletLifeTime);
        }

        public override void Death()
        {
            CancelInvoke(nameof(Death));

            angleRad = default;
            translate = Vector3.zero;
            _bulletDamage = default;
            _bulletSpeed = default;

            base.Death();
        }

        public Bullet Set(Vector3 position, Quaternion rotation, float speed, float damage, float lifetime, float slow = 0, float dot = 0)
        {
            DeactiveSelf();

            transform.position = position;
            transform.rotation = rotation;
            _bulletSpeed = speed;
            _bulletDamage = damage;
            _bulletLifeTime = lifetime;
            _slowTime = slow;
            _dotTime = dot;

            ActiveSelf();

            return this;
        }

        private void FixedUpdate()
        {
            transform.position += translate * _bulletSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyBase enemy))
            {
                enemy.DamagedByBullet(_bulletDamage);
                DeactiveSelf();
            }
            else
            {
                throw new Exception($"[Bullet]: EnemyBase가 아닌 물체와 트리거 충돌");
            }
        }
    }
}
