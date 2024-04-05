using UnityEngine;

namespace TetrisDefence.Game.Map
{
    /// <summary>
    /// 노드의 기본 동작 인터페이스
    /// </summary>
    public interface INode
    {
        /// <summary>
        /// 노드의 인덱스 번호를 가져오는 함수
        /// </summary>
        /// <returns> 인덱스 번호 </returns>
        int GetIndex();

        /// <summary>
        /// 노드의 현재 위치를 가져오는 함수
        /// </summary>
        /// <returns> 현재 위치 </returns>
        Vector2 GetPosition();
    }
}
