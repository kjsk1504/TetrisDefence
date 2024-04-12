using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Enemy;
using UnityEngine;

namespace TetrisDefence.Game
{
    public class TowerController : PoolBase
    {
        public int TowerIndex { get; private set; } = default;
        public Bullet bulletPrefab;
        public GameObject muzzle;
        public Collider AttackRange;
        private int _tier = default;
        private int[] _tetris = new int[7];
        private float _attackSpeed = 1;
        private float _attackRange = 8;
        private int _attackNumber = 1;
        private float _attackDelay;
        public List<EnemyBase> targets;
        public List<Bullet> bullets;


        private void Awake()
        {
            TowerManager.Instance.Register(this);
            TowerIndex = TowerManager.Instance.towers.Count + 1;
            AttackRange.transform.localScale = Vector3.one * _attackRange;
        }

        private void OnEnable()
        {
            transform.up = Vector3.up;
            transform.localPosition = new Vector3(0, 0, -0.4f);
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<EnemyBase>();

            if (!targets.Contains(target))
            {
                targets.Add(target);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (targets.Contains(other.gameObject.GetComponent<EnemyBase>()))
            {
                Vector3 direction = targets[0].transform.position - transform.position;
                direction.z = 0;
                transform.up = direction;
            }

            if (_attackDelay < _attackSpeed)
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.gameObject.GetComponent<EnemyBase>();

            if (targets.Contains(target))
            {
                targets.Remove(target);
            }

            if (targets.Count == 0)
            {
                transform.up = Vector3.up;
            }
        }

        private IEnumerator C_Shoot(int num)
        {
            Bullet[] bullets = PoolManager.Instance.GetBullets(num);

            for (int ix = 0; ix < num; ix++)
            {

                foreach (var bullet in bullets)
                {
                    bullet.gameObject.SetActive(false);
                    bullet.transform.SetParent(transform);
                    bullet.transform.position = muzzle.transform.position;
                    bullet.transform.rotation = transform.rotation;
                }

                print(ix);
                bullets[ix].gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(1.0f);
            }
            
        }
    }
}
