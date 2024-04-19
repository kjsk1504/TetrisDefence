using System.Collections.Generic;
using TetrisDefence.Data.Enums;
using TetrisDefence.Data.Utill;
using TetrisDefence.Game.Pool;
using UnityEngine;

namespace TetrisDefence.Data.Manager
{
    public class MobManager : SingletonMonoBase<MobManager>
    {
        public Transform StartPoint;
        public List<EnemyBase> EnemyList = new ();
        private int _stage;
        private int _wave;


        protected override void Awake()
        {
            base.Awake();

            StartPoint = GameObject.FindGameObjectWithTag("StartPoint").transform;
        }

        private void Start()
        {
            for (int ix = 3; ix < 9; ix++)
            {
                MobSpawn(ix);
            }
        }

        public void EnemyRegister(EnemyBase enemy)
        {
            EnemyList.Add(enemy);
        }

        public void EnemyUnregister(EnemyBase enemy)
        {
            EnemyList.Remove(enemy);
        }

        public void MobSpawn(int enemyIndex)
        {
            if (enemyIndex < 3 || enemyIndex > 8)
            {
                return;
            }

            var enemy = PoolManager.Instance.GetEnemy((EItem)enemyIndex).transform;
            enemy.parent = transform;
            enemy.position = StartPoint.position;
            enemy.gameObject.SetActive(true);
        }
    }
}
