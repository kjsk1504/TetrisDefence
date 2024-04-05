using UnityEngine;

namespace TetrisDefence.Game.Map
{
    /// <summary>
    /// 맵의 소켓 <br><see cref="MonoBehaviour"/>를 상속 받음</br>
    /// </summary>
    public class Socket : MonoBehaviour
    {
        /// <summary>
        /// 맵 정보(<see cref="MapOfNodes"/>)에 등록
        /// </summary>
        private void Awake()
        {
            MapOfNodes.Register(this);
        }
    }
}
