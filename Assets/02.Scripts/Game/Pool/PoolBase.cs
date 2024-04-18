using System;
using TetrisDefence.Data.Manager;
using UnityEngine;

namespace TetrisDefence.Game.Pool
{
    /// <summary>
    /// <see cref="UnityEngine.Pool.ObjectPool{T}"/>의 대상이 될 오브젝트들의 부모 클래스
    /// <br><see cref="MonoBehaviour"/>를 상속받음 </br>
    /// </summary>
    public class PoolBase : MonoBehaviour, IPool
    {
        public string poolIndex;
        public event Action<IPool> onBorn;
        public event Action<IPool> onDeath;


        protected virtual void OnEnable()
        {
            Born();
        }

        public virtual void Born()
        {
            onDeath = null;
            onBorn?.Invoke(this);
        }

        public virtual void Death()
        {
            onDeath?.Invoke(this);
            PoolManager.Instance.Release(this);
        }

        protected PoolBase ActiveSelf()
        {
            gameObject.SetActive(true);

            return this;
        }

        protected PoolBase DeactiveSelf()
        {
            gameObject.SetActive(false);

            return this;
        }

        protected PoolBase ToggleSelf()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }

            return this;
        }
    }
}
