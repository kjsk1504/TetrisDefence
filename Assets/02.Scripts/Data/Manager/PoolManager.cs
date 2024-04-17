using System;
using System.Collections.Generic;
using TetrisDefence.Enums;
using TetrisDefence.Game;
using TetrisDefence.Game.Enemy;
using TetrisDefence.Game.Mino;
using TetrisDefence.Data.Utill;
using UnityEngine;
using UnityEngine.Pool;

namespace TetrisDefence.Data.Manager
{
    public class PoolManager : SingletonMonoBase<PoolManager>
    {
        public PooledObjects itemObjects;
        public PooledObjects minoObjects;
        public PoolBase[] ItemPrefabs;
        public MinoBase[] MinoPrefabs;
        public Dictionary<EItem, ObjectPool<PoolBase>> pooledItems = new ();
        public Dictionary<EMino, ObjectPool<MinoBase>> pooledMinos = new ();


        protected override void Awake()
        {
            base.Awake();

            EItem[] items = (EItem[])Enum.GetValues(typeof(EItem));

            foreach (var item in items)
            {
                CreatePool
                (
                    item,
                    () =>
                    {
                        var pool = Instantiate(ItemPrefabs[(int)item].GetComponent<PoolBase>());
                        pool.name = pool.name.Replace("(Clone)", "");
                        return pool;
                    },
                    actionOnGet: (pool) =>
                    {
                        pool.transform.SetParent(itemObjects.transforms[(int)item]);
                        pool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        pool.gameObject.SetActive(false);
                    },
                    actionOnRelease: (pool) =>
                    {
                        StopAllCoroutines();
                        pool.transform.SetParent(itemObjects.transforms[(int)item]);
                        pool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        pool.gameObject.SetActive(false);
                    }
                );
            }

            EMino[] minos = (EMino[])Enum.GetValues(typeof(EMino));

            foreach (var mino in minos)
            {
                CreateMino
                (
                    mino,
                () =>
                {
                        var pool = Instantiate(MinoPrefabs[(int)mino].GetComponent<MinoBase>());
                        pool.name = pool.name.Replace("(Clone)", "");
                        return pool;
                    },
                    actionOnGet: (pool) =>
                    {
                        pool.transform.SetParent(minoObjects.transforms[(int)mino]);
                        pool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        pool.gameObject.SetActive(true);
                    },
                    actionOnRelease: (pool) =>
                    {
                        pool.transform.SetParent(minoObjects.transforms[(int)mino]);
                        pool.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        pool.gameObject.SetActive(false);
                    }
                );
            }
        }

        public void CreatePool(EItem key, 
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

        public PoolBase[] GetItems(EItem item, int number = 1)
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
            return pooledItems[EItem.Tower].Get().GetComponent<TowerController>();
        }

        public EnemyBase GetEnemy(int itemIndex)
        {
            if (itemIndex < 3 || itemIndex > 8)
            {
                Debug.Log($"{itemIndex}은 3~8사이여야 합니다.");
                return default;
            }

            return pooledItems[(EItem)itemIndex].Get().GetComponent<EnemyBase>();
        }

        public Bullet[] GetBullets(int num)
        {
            Bullet[] bullets = new Bullet[num];

            for (int ix = 0; ix < num; ix++)
            {
                bullets[ix] = (Bullet)pooledItems[EItem.Bullet].Get();
            }

            return bullets;
        }

        public Bullet GetBullet()
        {
            return (Bullet)pooledItems[EItem.Bullet].Get();
        }

        public void Release(PoolBase item)
        {
            pooledItems[Enum.Parse<EItem>(item.name)].Release(item);
        }

        public void CreateMino(EMino key,
                               Func<MinoBase> createFunc,
                               Action<MinoBase> actionOnGet = null,
                               Action<MinoBase> actionOnRelease = null,
                               Action<MinoBase> actionOnDestroy = null,
                               int maxSize = 1000)
        {
            if (!pooledMinos.ContainsKey(key))
            {
                ObjectPool<MinoBase> newPool = new ObjectPool<MinoBase>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, maxSize: maxSize);
                pooledMinos[key] = newPool;
            }
            else
            {
                Debug.LogError($"[PoolManager]: Can't create. {key} is alreay created");
            }
        }

        public MinoBase GetMino(EMino mino)
        {
            return pooledMinos[mino].Get();
        }
        public void Release(MinoBase mino)
        {
            pooledMinos[Enum.Parse<EMino>(mino.name)].Release(mino);
        }
    }
}
