using NUnit.Framework;
using System.Collections.Generic;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Map;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TetrisDefence.Game.Pool
{
    public class TowerPurchase : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public Transform buyTowerPosition;
        public Tower towerPrefab;
        public int price;
        private bool _isDrag = false;


        public void OnBeginDrag(PointerEventData eventData)
        {
            print($"[{name}]: begin");
            if (GameManager.Instance.MoneyChange(-price))
            {
                print($"[{name}]: get");
                _isDrag = true;
                towerPrefab = PoolManager.Instance.GetTower();
                towerPrefab.transform.SetParent(buyTowerPosition);
                towerPrefab.transform.position = buyTowerPosition.position;
                towerPrefab.gameObject.SetActive(true);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDrag)
            {
                towerPrefab.transform.Translate(eventData.delta/38.5f);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDrag)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(InputManager.Instance.MousePosition), out var hit))
                {
                    print("physics raycast hit: " + hit.transform.name);
                    towerPrefab.Mounting(hit.transform.GetComponent<TowerNode>());
                    return;
                }

                towerPrefab.Death();
                towerPrefab = null;

                _isDrag = false;
            }
        }
    }
}
