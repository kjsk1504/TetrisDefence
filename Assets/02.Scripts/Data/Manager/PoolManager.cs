using System;
using System.Collections.Generic;
using TetrisDefence.Data.Utill;
using TetrisDefence.Game;
using UnityEngine;
using UnityEngine.Pool;

namespace TetrisDefence.Data.Manager
{
    public interface IPoolItem
    {
    }

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
        public PooledObject pooledObject;
        public Dictionary<Item, ObjectPool<IPoolItem>> pooledItems = new ();


        protected override void Awake()
        {
            base.Awake();

            pooledObject = FindFirstObjectByType<PooledObject>();
        }

        public void CreatePool(Item key, 
                               Func<IPoolItem> createFunc,
                               Action<IPoolItem> actionOnGet = null,
                               Action<IPoolItem> actionOnRelease = null,
                               Action<IPoolItem> actionOnDestroy = null, 
                               int maxSize = 1000)
        {
            if (!pooledItems.ContainsKey(key))
            {
                ObjectPool<IPoolItem> newPool = new ObjectPool<IPoolItem>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, maxSize: maxSize);
                pooledItems[key] = newPool;
            }
            else
            {
                Debug.LogError($"[PoolManager]: Can't create. {key} is alreay created");
            }
        }
    }
}
