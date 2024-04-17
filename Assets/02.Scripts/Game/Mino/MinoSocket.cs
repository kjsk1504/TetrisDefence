using TetrisDefence.Data;
using UnityEngine;

namespace TetrisDefence.Game.Mino
{
    public class MinoSocket : SocketBase
    {
        public Transform minos;
        // todo: 캔버스를 가져오는 방법을 좀더 세련되게 하기.
        public Canvas canvas;

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }
}
