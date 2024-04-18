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
        public EMino mino;
        public MinoBase minoPrefab;
        public TMP_Text textQuantity;
        public bool isDrag;
        private int _quantity = 0;


        protected virtual void Awake()
        {
            isDrag = false;
        }

        private void Start()
        {
            InventoryManager.Instance.onQuantitiesChanged[(int)mino] += (delta) => 
                {
                    _quantity += delta;
                    textQuantity.text = _quantity.ToString("d3");
                };
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_quantity > 0)
            {
                InventoryManager.Instance.ItemUpdate((int)mino, -1);
                minoPrefab = PoolManager.Instance.GetMino(mino);
                minoPrefab.transform.SetParent(transform);
                minoPrefab.transform.position = transform.position;
                minoPrefab.gameObject.SetActive(true);
                isDrag = true;
            }
            else
            {
                UIManager.Instance.Get<UINotifyingWindow>().Show("수량 부족");
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDrag)
            {
                minoPrefab.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isDrag)
            {
                RaycastResult result = eventData.pointerCurrentRaycast;
                print($"[UIItemBase]: RaycastResult {result}");
                if (result.gameObject != null)
                {
                    var canvas = result.gameObject.GetComponentInParent<UITowerInformation>();
                    if (canvas != null)
                    {
                        canvas.RegisterMino(minoPrefab);
                        minoPrefab.transform.SetParent(canvas.minos);
                        minoPrefab.transform.position = eventData.position;
                        isDrag = false;

                        return;
                    }
                }

                minoPrefab.Death();
                InventoryManager.Instance.ItemUpdate((int)mino, +1);
            }
        }
    }
}
