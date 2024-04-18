using System;
using TetrisDefence.Data.Manager;
using TetrisDefence.Data.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TetrisDefence.Game.Pool
{
    public class MinoBase : PoolBase, IDragHandler
    {
        public EMino minoIndex;
        private RectTransform _rectTransform;
        // todo: 미노의 위치, 크기, 회전각 등을 저장해서 타워인포에 전달
        // todo: 미노의 드래그가 끝나면 타워 소켓의 위치에 알아서 맞춰서 붙기


        private void Awake()
        {
            if (minoIndex == 0)
            {
                if (!Enum.TryParse(name, out minoIndex))
                {
                    print($"[MinoBase]: {name}: {minoIndex} error");
                }
            }

            poolIndex = minoIndex.ToString();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
            print(transform.position);
        }

        public override void Born()
        {
            if (minoIndex == 0)
            {
                if (!Enum.TryParse(name, out minoIndex))
                {
                    print($"[MinoBase]: {name}: {minoIndex} error");
                }
            }

            base.Born();
        }
    }
}
