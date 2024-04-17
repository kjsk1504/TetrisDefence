using System;
using TetrisDefence.Data.Manager;
using TetrisDefence.Enums;
using Unity.Collections;
using UnityEngine;

namespace TetrisDefence.Game
{
    /// <summary>
    /// <see cref="UnityEngine.Pool.ObjectPool{T}"/>의 대상이 될 오브젝트들의 부모 클래스
    /// <br><see cref="MonoBehaviour"/>를 상속받음 </br>
    /// </summary>
    public class PoolBase : MonoBehaviour, IPool
    {
        public EItem ItemIndex { get { return _itemIndex; } set { _itemIndex = value; } }
        public event Action onBorn;
        public event Action<IPool> onDeath;
        [ReadOnly, SerializeField] private EItem _itemIndex = default;


        protected virtual void OnEnable()
        {
            Born();
        }

        public virtual void Born()
        {
            onDeath = null;

            if (ItemIndex == EItem.None)
            {
                if (!Enum.TryParse(name, out _itemIndex))
                {
                    print($"[PoolBase]: {name} index error");
                }
            }

            onBorn?.Invoke();
        }

        public virtual void Death()
        {
            PoolManager.Instance.Release(this);

            onDeath?.Invoke(this);
        }
    }
}
