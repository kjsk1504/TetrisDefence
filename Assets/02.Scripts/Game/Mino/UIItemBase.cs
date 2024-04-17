using TetrisDefence.Data.Manager;
using TetrisDefence.Enums;
using TetrisDefence.Game.Map;
using TetrisDefence.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TetrisDefence.Game.Mino
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
                minoPrefab.transform.position = eventData.position;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isDrag)
            {
                RaycastResult result = eventData.pointerCurrentRaycast;
                print(result);
                if (result.gameObject != null && result.gameObject.TryGetComponent<MinoSocket>(out var socket))
                {
                    minoPrefab.transform.SetParent(socket.minos);
                    minoPrefab.transform.position = eventData.position;
                    socket.canvas.GetComponent<UITowerInformation>().minos.Add(minoPrefab);
                    isDrag = false;
                }
                else
                {
                    minoPrefab.Death();
                    InventoryManager.Instance.ItemUpdate((int)mino, +1);
                }
            }
        }
    }
}
