using System;
using System.Collections.Generic;
using TetrisDefence.Data.Enums;
using TetrisDefence.Data.Utill;
using TetrisDefence.Game.Pool;
using UnityEngine;
using UnityEngine.Pool;

namespace TetrisDefence.Data.Manager
{
    public class PoolManager : SingletonMonoBase<PoolManager>
    {
        public PooledObjects itemObjects;
        public PooledObjects minoObjects;
        public ItemBase[] ItemPrefabs;
        public MinoBase[] MinoPrefabs;
        public Dictionary<EItem, ObjectPool<ItemBase>> pooledItems = new ();
        public Dictionary<EMino, ObjectPool<MinoBase>> pooledMinos = new ();


        protected override void Awake()
        {
            base.Awake();

            EItem[] itemIndexes = (EItem[])Enum.GetValues(typeof(EItem));

            foreach (var itemIndex in itemIndexes)
            {
                CreatePool
                (
                    itemIndex,
                    () =>
                    {
                        var item = Instantiate(ItemPrefabs[(int)itemIndex].GetComponent<ItemBase>());
                        item.transform.SetParent(itemObjects.transforms[(int)itemIndex]);
                        item.gameObject.SetActive(false);
                        return item;
                    },
                    actionOnGet: (item) =>
                    {
                        item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        item.gameObject.SetActive(false);
                    },
                    actionOnRelease: (item) =>
                    {
                        StopAllCoroutines();
                        item.transform.SetParent(itemObjects.transforms[(int)itemIndex]);
                        item.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        item.gameObject.SetActive(false);
                    },
                    actionOnDestroy: (item) =>
                    {
                        print(item);
                        Release(item);
                    }
                );
            }

            EMino[] minoIndexes = (EMino[])Enum.GetValues(typeof(EMino));

            foreach (var minoIndex in minoIndexes)
            {
                CreateMino
                (
                    minoIndex,
                    () =>
                    {
                        var mino = Instantiate(MinoPrefabs[(int)minoIndex].GetComponent<MinoBase>());
                        mino.transform.SetParent(minoObjects.transforms[(int)minoIndex]);
                        mino.gameObject.SetActive(false);
                        return mino;
                    },
                    actionOnGet: (mino) =>
                    {
                        mino.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        mino.gameObject.SetActive(false);
                    },
                    actionOnRelease: (mino) =>
                    {
                        StopAllCoroutines();
                        mino.transform.SetParent(minoObjects.transforms[(int)minoIndex]);
                        mino.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
                        mino.gameObject.SetActive(false);
                    },
                    actionOnDestroy: (item) =>
                    {
                        print(item);
                        Release(item);
                    }
                );
            }
        }

        public void CreatePool(EItem itemIndexKey, 
                               Func<ItemBase> createFunc,
                               Action<ItemBase> actionOnGet = null,
                               Action<ItemBase> actionOnRelease = null,
                               Action<ItemBase> actionOnDestroy = null, 
                               int maxSize = 1000)
        {
            if (!pooledItems.ContainsKey(itemIndexKey))
            {
                ObjectPool<ItemBase> newPool = new ObjectPool<ItemBase>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, maxSize: maxSize);
                pooledItems[itemIndexKey] = newPool;
            }
            else
            {
                Debug.LogError($"[PoolManager - Create]: Item {itemIndexKey}는 이미 만들어져 있음");
            }
        }

        public void CreateMino(EMino minoIndexKey,
                               Func<MinoBase> createFunc,
                               Action<MinoBase> actionOnGet = null,
                               Action<MinoBase> actionOnRelease = null,
                               Action<MinoBase> actionOnDestroy = null,
                               int maxSize = 1000)
        {
            if (!pooledMinos.ContainsKey(minoIndexKey))
            {
                ObjectPool<MinoBase> newPool = new ObjectPool<MinoBase>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, maxSize: maxSize);
                pooledMinos[minoIndexKey] = newPool;
            }
            else
            {
                Debug.LogError($"[PoolManager - Create]: Mino {minoIndexKey}는 이미 만들어져 있음");
            }
        }

        public T[] Get<T> (int number = 1, Action<IPool> onBorn = null, Action<IPool> onDeath = null) where T : PoolBase
        {
            T[] pools = new T[number];

            if(Enum.TryParse(nameof(T), out EItem itemIndex))
            {
                for (int ix = 0; ix < number; ix++)
                {
                    pools[ix] = (T)pooledItems[itemIndex].Get().GetComponent<PoolBase>();
                    if (onBorn != null) pools[ix].onBorn += onBorn;
                    if (onDeath != null) pools[ix].onDeath += onDeath;
                }
            }
            else if(Enum.TryParse(nameof(T), out EMino minoIndex))
            {
                for (int ix = 0; ix < number; ix++)
                {
                    pools[ix] = (T)pooledMinos[minoIndex].Get().GetComponent<PoolBase>();
                    if (onBorn != null) pools[ix].onBorn += onBorn;
                    if (onDeath != null) pools[ix].onDeath += onDeath;
                }
            }
            else
            {
                throw new Exception($"[PoolManager - Get]: 해당 값이 등록되어 있지 않음");
            }

            return pools;
        }

        public void Release(PoolBase pool)
        {
            if (Enum.TryParse(pool.PoolIndex, out EItem itemIndex))
            {
                pooledItems[itemIndex].Release((ItemBase)pool);
            }
            else if (Enum.TryParse(pool.PoolIndex, out EMino minoIndex))
            {
                pooledMinos[minoIndex].Release((MinoBase)pool);
            }
            else
            {
                throw new Exception($"[PoolManager - Release]: 해당 값이 등록되어 있지 않음");
            }
        }

        public ItemBase[] GetItems(EItem itemIndex, int number = 1, Action<IPool> onBorn = null, Action<IPool> onDeath = null)
        {
            ItemBase[] items = new ItemBase[number];

            for (int ix = 0; ix < number; ix++)
            {
                items[ix] = pooledItems[itemIndex].Get();
                if (onBorn != null) items[ix].onBorn += onBorn;
                if (onDeath != null) items[ix].onDeath += onDeath;
            }

            return items;
        }

        public MinoBase[] GetMinos(EMino minoIndex, int number = 1, Action<IPool> onBorn = null, Action<IPool> onDeath = null)
        {
            MinoBase[] minos = new MinoBase[number];

            for (int ix = 0; ix < number; ix++)
            {
                MinoBase mino = pooledMinos[minoIndex].Get();
                if (onBorn != null) mino.onBorn += onBorn;
                if (onDeath != null) mino.onDeath += onDeath;
            }

            return minos;
        }

        public Bullet[] GetBullets(int number, Action<IPool> onBorn = null, Action<IPool> onDeath = null)
        {
            Bullet[] bullets = new Bullet[number];

            for (int ix = 0; ix < number; ix++)
            {
                bullets[ix] = (Bullet)pooledItems[EItem.Bullet].Get();
                if (onBorn != null) bullets[ix].onBorn += onBorn;
                if (onDeath != null) bullets[ix].onDeath += onDeath;
            }

            return bullets;
        }

        public Bullet GetBullet(Action<IPool> onBorn = null, Action<IPool> onDeath = null)
        {
            Bullet bullet = (Bullet)pooledItems[EItem.Bullet].Get();

            if (onBorn != null) bullet.onBorn += onBorn;
            if (onDeath != null) bullet.onDeath += onDeath;

            return bullet;
        }

        public EnemyBase GetEnemy(EItem enemyIndex, Action<IPool> onBorn = null, Action<IPool> onDeath = null)
        {
            if ((int)enemyIndex < 3 || (int)enemyIndex > 8)
            {
                Debug.Log($"{enemyIndex}은 3~8사이여야 합니다.");
                return default;
            }

            EnemyBase enemy = (EnemyBase)pooledItems[enemyIndex].Get();

            if (onBorn != null) enemy.onBorn += onBorn;
            if (onDeath != null) enemy.onDeath += onDeath;

            return enemy;
        }

        public Tower GetTower(Action<IPool> onBorn = null, Action<IPool> onDeath = null)
        {
            Tower tower = (Tower)pooledItems[EItem.Tower].Get();

            if (onBorn != null) tower.onBorn += onBorn;
            if (onDeath != null) tower.onDeath += onDeath;

            return tower;
        }

        public void ReleaseItem(ItemBase item)
        {
            pooledItems[item.itemIndex].Release(item);
        }

        public MinoBase GetMino(EMino minoIndex, Action<IPool> onBorn = null, Action<IPool> onDeath = null)
        {
            MinoBase mino = pooledMinos[minoIndex].Get();

            if (onBorn != null) mino.onBorn += onBorn;
            if (onDeath != null) mino.onDeath += onDeath;

            return mino;
        }

        public void ReleaseMino(MinoBase mino)
        {
            pooledMinos[mino.minoIndex].Release(mino);
        }
    }
}
