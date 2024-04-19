using TetrisDefence.Data.Manager;
using TetrisDefence.Data.Enums;
using TetrisDefence.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Net.Sockets;

namespace TetrisDefence.Game.Pool
{
    public class UIItemBase : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public EMino minoIndex;
        public MinoBase minoPrefab;
        public TMP_Text textQuantity;
        private int _quantity = 0;


        private void Start()
        {
            InventoryManager.Instance.onQuantitiesChanged[(int)minoIndex] += (delta) => 
                {
                    _quantity += delta;
                    textQuantity.text = _quantity.ToString("d3");
                };
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_quantity > 0)
            {
                InventoryManager.Instance.ItemUpdate((int)minoIndex, -1);
                minoPrefab = PoolManager.Instance.GetMino(minoIndex);
                minoPrefab.transform.SetParent(transform);
                minoPrefab.transform.position = transform.position;
                minoPrefab.gameObject.SetActive(true);
                minoPrefab.isDrag = true;

                if (TowerManager.Instance.isUIActive)
                {
                    minoPrefab.transform.SetParent(TowerManager.Instance.towerInfoUI.minos);
                }
            }
            else
            {
                UIManager.Instance.Get<UINotifyingWindow>().Show("수량 부족");
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            minoPrefab.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            minoPrefab.OnEndDrag(eventData);
            minoPrefab = null;
        }
    }
}
