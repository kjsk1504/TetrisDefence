using System.Collections.Generic;
using System.Linq;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Enemy;
using TetrisDefence.Game.Map;
using UnityEngine;

namespace TetrisDefence.Game
{
    public class TowerController : PoolBase
    {
        [field: SerializeField] public int TowerIndex { get; set; } = default;
        [field: SerializeField] public int[] TowerLocation { get; private set; } = new int[2];
        [field: SerializeField] public int Tier { get; private set; } = default;

        public Bullet bulletPrefab;
        public GameObject barrel;
        public GameObject muzzle;
        public Collider attackRange;
        public List<EnemyBase> targets;
        public List<Bullet> bullets;

        private TowerInfo _towerinfo = new ();
        private float _attackDelay = 0;


        private void Awake()
        {
            _towerinfo = new TowerInfo();

            if (!attackRange)
            {
                attackRange = GetComponentInChildren<Collider> ();
            }

            attackRange.transform.localScale = Vector3.one * _towerinfo.AttackRange;
        }

        private void Start()
        {
            TowerLocation = transform.GetComponentInParent<NodeSocket>().Location;
            _towerinfo = new TowerInfo(TowerIndex, TowerLocation, Tier, new int[7]);
        }

        private void Update()
        {
            _attackDelay += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            var bulletArray = bullets.ToArray();
            foreach (var bullet in bulletArray)
            {
                if (Mathf.Pow(bullet.transform.localPosition.x, 2) + Mathf.Pow(bullet.transform.localPosition.y, 2) > Mathf.Pow(_towerinfo.AttackRange / 2, 2))
                {
                    bullet.Death();
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<EnemyBase>();

            if (!targets.Contains(target))
            {
                targets.Add(target);
                target.onDeath += IfDeadRemoveFromList;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (targets.Contains(other.gameObject.GetComponent<EnemyBase>()))
            {
                Vector3 direction = targets[0].transform.position - barrel.transform.position;
                direction.z = 0;
                barrel.transform.up = direction;
            }

            if (_attackDelay > _towerinfo.AttackCooldown)
            {
                Shoot();
                _attackDelay = 0;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var target = other.gameObject.GetComponent<EnemyBase>();

            if (targets.Contains(target))
            {
                targets.Remove(target);
            }
        }

        public override void Born()
        {
            base.Born();

            TowerManager.Instance.Register(this);
            transform.up = Vector3.up;
            transform.localPosition = new Vector3(0, 0, -0.4f);
        }

        public override void Death()
        {
            base.Death();

            TowerManager.Instance.Unregister(this);
        }

        public void Shoot()
        {
            var bullet = PoolManager.Instance.GetBullet();
            bullet.gameObject.SetActive(false);

            bullets.Add(bullet);
            bullet.transform.SetParent(transform);
            bullet.transform.position = muzzle.transform.position;
            bullet.transform.rotation = barrel.transform.rotation;
            bullet.bulletSpeed = _towerinfo.AttackSpeed;
            bullet.onDeath += IfDeadRemoveFromList;

            bullet.gameObject.SetActive(true);
        }

        private void IfDeadRemoveFromList(IPool pool)
        {
            if (targets.Contains(pool))
            {
                targets.Remove((EnemyBase)pool);
            }
            else if (bullets.Contains(pool))
            {
                bullets.Remove((Bullet)pool);
            }
        }

        private void OnMouseDown()
        {
            TowerManager.Instance.TowerSelection(_towerinfo);
        }

        private void OnMouseUp()
        {
            
        }
    }
}
