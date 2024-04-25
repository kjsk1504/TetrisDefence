using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisDefence.UI
{
    /// <summary>
    /// 플로팅 창 (마우스를 오버했을시 뜨는 창)
    /// <br><see cref="UIScreenBase"/>를 상속 받음</br>
    /// </summary>
    public class UIFloatingWindow : UIScreenBase
    {
        /// <summary> 패널의 위치 </summary>
        public Transform pannel;
        /// <summary> 제목 영역 </summary>
        private TMP_Text _title;
        /// <summary> 메세지 영역 </summary>
        private TMP_Text _message;
        /// <summary> 툴팁 영역 </summary>
        private TMP_Text _tooltip;


        protected override void Awake()
        {
            base.Awake();

            pannel = transform.Find("Panel").GetComponent<Transform>();
            _message = transform.Find("Panel/Text (TMP) - Message").GetComponent<TMP_Text>();
            _title = transform.Find("Panel/Text (TMP) - Title").GetComponent<TMP_Text>();
            _tooltip = transform.Find("Panel/Text (TMP) - ToolTip").GetComponent<TMP_Text>();
        }

        /// <summary>
        /// 플로팅 창을 띄움
        /// </summary>
        /// <param name="message"> 보여줄 메세지 </param>
        public void Show(string title, string message, string tooltip, Color color, float height = 100)
        {
            color.a = 0.8f;
            pannel.gameObject.GetComponent<Image>().color = color;
            var p = (RectTransform)pannel;
            p.sizeDelta = new Vector2(p.sizeDelta.x, height);
            _title.text = title;
            _message.text = message;
            _tooltip.text = tooltip;
            _tooltip.color = new Color(1 - color.r, 1 - color.g, 1 - color.b);

            base.Show();
        }
    }
}