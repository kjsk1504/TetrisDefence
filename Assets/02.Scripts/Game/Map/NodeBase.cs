using System;
using UnityEngine;

namespace TetrisDefence.Game.Map
{
    /// <summary>
    /// 노드 베이스
    /// <br>유니티의 <see cref="MonoBehaviour"/>와 <see cref="INode"/>, <see cref="IComparable{T}"/>를 상속 받음</br>
    /// <para>도로 노드(<see cref = "RoadNode"/>)와 타워 노드(<see cref = "TowerNode"/>)의 기본이 되는 부모 클래스</para>
    /// </summary>
    public class NodeBase : MonoBehaviour, INode, IComparable<NodeBase>
    {
        /// <summary> 노드를 구별하기 위한 인덱스 번호 </summary>
        public int NodeIndex;
        /// <summary> 노드의 위치 좌표 </summary>
        public Vector3 NodePosition { get; private set; } = default;


        /// <summary> <see cref = "NodeIndex"/>를 <seealso cref="Array.Sort"/>하기 위해 구현한 <see cref = "IComparable{T}"/>의 필수 함수 </summary>
        /// <param name="other"> 비교할 대상 </param>
        /// <returns> 비교 결과 <para> 
        /// <br> 0 이하 : 비교 대상보다 전에 위치 </br>
        /// <br> 0 일때 : 비교 대상하고 같은 위치 </br>
        /// <br> 0 이상 : 비교 대상보다 후에 위치 </br>
        /// </para> </returns>
        public int CompareTo(NodeBase other)
        {
            if (NodeIndex < other.NodeIndex)
            {
                return -1;
            }
            else if (NodeIndex > other.NodeIndex)
            {
                return 1;
            }

            return 0;
        }

        public int GetIndex()
        {
            string indexString = string.Join("", gameObject.name[gameObject.name.Length - 3], gameObject.name[gameObject.name.Length - 2]);

            if (int.TryParse(indexString, out int nodeIndex))
            {
                return nodeIndex;
            }
            else
            {
                Debug.LogError($"{indexString}은 (00)형식으로 끝나야함");
                return default;
            }
        }

        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }

        public bool ComparePosition(float xPosition, float yPosition)
        {
            return (transform.position.x - 27.5f < xPosition) && (transform.position.x + 27.5 > xPosition)
                && (transform.position.y - 27.5f < yPosition) && (transform.position.y + 27.5 > yPosition);
        }

        public bool ComparePosition(Vector2 position)
        {
            return ComparePosition(position.x, position.y);
        }

        public bool ComparePosition(Vector3 position)
        {
            return ComparePosition(position.x, position.y);
        }

        /// <summary>
        /// 맵 정보(<see cref="MapOfNodes"/>)에 등록
        /// <br>게임 오브젝트의 이름에서 인덱스 번호(<see cref="NodeIndex"/>)를 추출하고, 현재 위치를 위치 좌표(<see cref="_position"/>)로 초기화</br>
        /// </summary>
        protected virtual void Awake()
        {
            NodeIndex = GetIndex();
            NodePosition = GetPosition();
        }
    }
}
