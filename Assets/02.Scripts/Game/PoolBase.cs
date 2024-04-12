using System;
using TetrisDefence.Data.Manager;
using UnityEngine;

namespace TetrisDefence.Game
{
    /// <summary>
    /// <see cref="UnityEngine.Pool.ObjectPool{T}"/>의 대상이 될 오브젝트들의 부모 클래스
    /// <br><see cref="MonoBehaviour"/>를 상속받음 </br>
    /// </summary>
    public class PoolBase : MonoBehaviour
    {
        protected Item _itemIndex = default;

        public virtual void Born()
        {
            name = name.Replace("(Clone)", "");

            if (!Enum.TryParse(name, out _itemIndex))
            {
                print($"{name}: {_itemIndex} error");
            }
        }

        public virtual void Death()
        {
            StopAllCoroutines();
            PoolManager.Instance.Release(this);
        }
    }
}
