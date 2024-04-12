using System.Collections;
using TetrisDefence.Data.Utill;
using TetrisDefence.Game.Enemy;
using UnityEngine;

namespace TetrisDefence.Game
{
    public class Bullet : PoolBase
    {
        private float _bulletSpeed = 1.0f;
        private float _bulletTime = 10.0f;


        private void OnEnable()
        {
            base.Born();

            Invoke(nameof(Death), _bulletTime);
        }

        private void Update()
        {
            transform.position = transform.position + transform.up * _bulletSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (TryGetComponent(out EnemyBase enemy))
            {
                enemy.Death();
                Death();
            }
            else
            {
                throw new System.Exception("[Bullet]: somthing is wrong");
            }
        }
    }
}
