using System;
using TetrisDefence.Data.Manager;
using TetrisDefence.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TetrisDefence.Game.Mino
{
    public class MinoBase : MonoBehaviour, IPool, IDragHandler
    {
        public EMino mino;
        public event Action onBorn;
        public event Action<IPool> onDeath;
        private RectTransform _rectTransform;


        protected virtual void Awake()
        {
            if (mino == 0)
            {
                if (!Enum.TryParse(name, out mino))
                {
                    print($"[MinoBase]: {name}: {mino} error");
                }
            }

            _rectTransform = GetComponent<RectTransform>();
        }

        protected virtual void OnEnable()
        {
            Born();
        }

        public void Born()
        {
            onDeath = null;

            if (mino == 0)
            {
                if (!Enum.TryParse(name, out mino))
                {
                    print($"[MinoBase]: {name}: {mino} error");
                }
            }

            onBorn?.Invoke();
        }

        public void Death()
        {
            PoolManager.Instance.Release(this);

            onDeath?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
        }

    }
}
