using TetrisDefence.Data.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TetrisDefence.UI
{
    public class MouseOverable : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
    {
        public string title = "제목";
        public string message = "메세지";
        public string tooltip = "*툴팁";
        private UIFloatingWindow _floatingWindow;


        private void Start()
        {
            _floatingWindow = UIManager.Instance.Get<UIFloatingWindow>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _floatingWindow.Show(title, message, tooltip, GetComponent<Image>().color);
            _floatingWindow.pannel.transform.position = eventData.position;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _floatingWindow.pannel.transform.position = eventData.position;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _floatingWindow.Hide();
        }
    }
}
