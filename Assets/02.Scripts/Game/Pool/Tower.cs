using System.Collections.Generic;
using System.Linq;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Map;
using UnityEngine;

namespace TetrisDefence.Game.Pool
{
    public class Tower : ItemBase
    {
        [field: SerializeField] public int TowerIndex { get; set; } = default;
        [field: SerializeField] public int[] TowerLocation { get; private set; } = new int[2];
        [field: SerializeField] public int Tier { get; private set; } = default;

        public Bullet bulletPrefab;
        public GameObject barrel;
        public GameObject muzzle;
        public Collider attackRange;

        private TowerInfo _towerinfo = new ();
        private float _attackDelay = 0;
        [SerializeField] private List<EnemyBase> _targets;
        [SerializeField] private List<Bullet> _bullets;


        private void Awake()
        {
            if (!attackRange)
            {
                attackRange = GetComponentInChildren<Collider>();
            }
        }

        private void Start()
        {
            TowerLocation = transform.GetComponentInParent<NodeSocket>().Location;
            _towerinfo = new TowerInfo(TowerIndex, TowerLocation, Tier, new int[7]);
            attackRange.transform.localScale = Vector3.one * _towerinfo.AttackRange;
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

            if (_attackDelay > _towerinfo.AttackCooldown)
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
            TowerManager.Instance.TowerSelection(_towerinfo);
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
                .Set(muzzle.transform.position, barrel.transform.rotation, _towerinfo.AttackSpeed, _towerinfo.AttackDamage, 
                     (_towerinfo.AttackRange/2 - 1) / _towerinfo.AttackSpeed, _towerinfo.SlowTime, _towerinfo.DotTime)
                .transform.SetParent(transform);
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
    }
}
