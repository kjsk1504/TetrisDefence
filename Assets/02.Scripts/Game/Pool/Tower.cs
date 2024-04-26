using System.Collections.Generic;
using System.Linq;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Map;
using UnityEngine;

namespace TetrisDefence.Game.Pool
{
    public class Tower : ItemBase
    {
        [field: SerializeField] public int[] TowerLocation { get; private set; } = new int[2];
        [field: SerializeField] public int Tier { get; private set; } = 1;
        [field: SerializeField] public bool IsMouted { get; private set; } = false;

        public int towerIndex = default;
        public Bullet bulletPrefab;
        public GameObject barrel;
        public GameObject muzzle;
        public Collider attackRange;
        public TowerInfo towerInfo = new ();

        private float _attackDelay = 0;
        [SerializeField] private List<EnemyBase> _targets;
        [SerializeField] private List<Bullet> _bullets;


        protected override void Awake()
        {
            base.Awake();

            if (!attackRange)
            {
                attackRange = GetComponentInChildren<Collider>();
            }

            attackRange.enabled = false;
        }

        private void Update()
        {
            _attackDelay += Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            var target = other.gameObject.GetComponent<EnemyBase>();
            AddToList(target);
            target.onDeath += RemoveFromList;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_targets.Contains(other.gameObject.GetComponent<EnemyBase>()))
            {
                Vector3 direction = _targets[0].transform.position - barrel.transform.position;
                direction.z = 0;
                barrel.transform.up = direction;
            }

            if (_attackDelay > towerInfo.AttackCooldown)
            {
                Shoot();
                _attackDelay = 0;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            RemoveFromList(other.gameObject.GetComponent<EnemyBase>());
        }

        private void OnMouseDown()
        {
            TowerManager.Instance.TowerSelection(this);
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
            PoolManager.Instance.GetBullet(onBorn: AddToList, onDeath: RemoveFromList)
                .Set(muzzle.transform.position, barrel.transform.rotation, towerInfo.AttackSpeed, towerInfo.AttackDamage, 
                     (towerInfo.AttackRange/2 - 1) / towerInfo.AttackSpeed, towerInfo.SlowTime, towerInfo.DotTime)
                .transform.SetParent(transform);
        }

        public void Mounting(TowerNode towerNode)
        {
            towerNode.Mount(this);
            transform.position = towerNode.transform.position;
            TowerLocation = towerNode.Location;
            towerInfo = new TowerInfo(towerIndex, TowerLocation, Tier, new int[7], UpdateTower);
            attackRange.enabled = true;
            IsMouted = true;
        }

        public void Unmounting()
        {
            TowerManager.Instance.Unregister(this);
            IsMouted = false;
        }

        private void RemoveFromList(IPool pool)
        {
            if (_targets.Contains(pool))
            {
                _targets.Remove((EnemyBase)pool);
            }
            else if (_bullets.Contains(pool))
            {
                _bullets.Remove((Bullet)pool);
            }
        }

        private void AddToList(IPool pool)
        {
            if (pool is Bullet && !_bullets.Contains(pool))
            {
                _bullets.Add((Bullet)pool);
            }
            else if (pool is EnemyBase && !_targets.Contains(pool))
            {
                _targets.Add((EnemyBase)pool);
            }
        }
        private void UpdateTower()
        {
            attackRange.transform.localScale = Vector3.one * towerInfo.AttackRange;
            Tier = towerInfo.TowerTier;
        }
    }
}
