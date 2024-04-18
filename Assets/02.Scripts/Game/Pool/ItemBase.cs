using System;
using TetrisDefence.Data.Manager;
using TetrisDefence.Data.Enums;
using Unity.Collections;
using UnityEngine;

namespace TetrisDefence.Game.Pool
{
    /// <summary>
    /// <see cref="UnityEngine.Pool.ObjectPool{T}"/>의 대상이 될 오브젝트들의 부모 클래스
    /// <br><see cref="MonoBehaviour"/>를 상속받음 </br>
    /// </summary>
    public class ItemBase : PoolBase
    {
        public EItem itemIndex;

        private void Awake()
        {
            if (itemIndex == 0)
            {
                if (!Enum.TryParse(name, out itemIndex))
                {
                    print($"[MinoBase]: {name}: {itemIndex} error");
                }
            }

            poolIndex = itemIndex.ToString();
        }
    }
}
