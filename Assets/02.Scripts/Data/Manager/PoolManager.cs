using System;
using System.Collections.Generic;
using TetrisDefence.Data.Utill;
using TetrisDefence.Game;
using TetrisDefence.Game.Enemy;
using UnityEngine;
using UnityEngine.Pool;

namespace TetrisDefence.Data.Manager
{
    public enum Item
    {
        None,
        Bullet,
        Tower,
        TrigonalPrism,
        QuadgonalPrism,
        PentagonalPrism,
        HexagonalPrism,
        HeptagonalPrism,
        OctagonalPrism,
    }

    public class PoolManager : SingletonMonoBase<PoolManager>
    {
        private PooledObjects pooledObjects;
        public PoolBase[] poolPrefab;
        public Dictionary<Item, ObjectPool<PoolBase>> pooledItems = new ();


        protected override void Awake()
        {
            base.Awake();

            pooledObjects = FindFirstObjectByType<PooledObjects>();
            Item[] items = (Item[])Enum.GetValues(typeof(Item));

            foreach (Item item in items)
            {
                CreatePool
                (
                    item,
                    () =>
                    {
                        return Instantiate(poolPrefab[(int)item].GetComponent<PoolBase>());
                    },
                    actionOnGet: (pool) =>
                    {
                        pool.gameObject.SetActive(true);
                    },
                    actionOnRelease: (pool) =>
                    {
                        pool.transform.SetParent(pooledObjects.transform.GetChild((int)item));
                        pool.gameObject.SetActive(false);
                    }
                );
            }
        }

        public void CreatePool(Item key, 
                               Func<PoolBase> createFunc,
                               Action<PoolBase> actionOnGet = null,
                               Action<PoolBase> actionOnRelease = null,
                               Action<PoolBase> actionOnDestroy = null, 
                               int maxSize = 1000)
        {
            if (!pooledItems.ContainsKey(key))
            {
                ObjectPool<PoolBase> newPool = new ObjectPool<PoolBase>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, maxSize: maxSize);
                pooledItems[key] = newPool;
            }
            else
            {
                Debug.LogError($"[PoolManager]: Can't create. {key} is alreay created");
            }
        }

        public PoolBase[] GetItems(Item item, int number = 1)
        {
            PoolBase[] pools = new PoolBase[number];

            for (int ix = 0; ix < number; ix++)
            {
                pools[ix] = pooledItems[item].Get();
            }

            return pools;
        }

        public TowerController GetTower()
        {
            return pooledItems[Item.Tower].Get().GetComponent<TowerController>();
        }

        public EnemyBase GetEnemy(Item item)
        {
            if ((int)item < 3 || (int)item > 8)
            {
                Debug.Log($"{item}은 3~8사이여야 합니다.");
                return default;
            }

            return pooledItems[item].Get().GetComponent<EnemyBase>();
        }

        public void Release(PoolBase item)
        {
            pooledItems[Enum.Parse<Item>(item.name)].Release(item);
        }

        public Bullet[] GetBullets(int num)
        {
            Bullet[] bullets = new Bullet[num];

            for (int ix = 0; ix < num; ix++)
            {
                bullets[ix] = (Bullet)pooledItems[Item.Bullet].Get();
            }

            return bullets;
        }
    }
}
