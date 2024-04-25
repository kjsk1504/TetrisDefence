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
                //todo: 타워 노드 가져오는 법 생각하기
                print($"[{name}]: end");
                var result = eventData.pointerCurrentRaycast;
                print(result);
                if (result.gameObject)
                {
                    if (result.gameObject.TryGetComponent<TowerNode>(out var towerNode))
                    {
                        print($"[{name}]: mount");
                        transform.position = towerNode.GetPosition();
                        transform.SetParent(towerNode.transform.parent);
                    }
                }

                print("death");
                towerPrefab.Death();
                towerPrefab = null;

                _isDrag = false;
            }
        }
    }
}
