using UnityEngine;

namespace TetrisDefence.Game.Map
{
    /// <summary>
    /// 도로 노드
    /// <br>노드 베이스(<see cref="NodeBase"/>)를 상속받음</br>
    /// <para> 직진 도로(<see cref="RoadStraight"/>)와 좌회전 도로(<see cref="RoadLeftturn"/>)의 부모 클래스 </para>
    /// </summary>
    public class RoadNode : NodeBase
    {
        /// <summary> 적 오브젝트가 위에 있는지 여부 </summary>
        public bool OnEnemy { get; private set; } = default;
        /// <summary> 입구 위치 좌표 </summary>
        public Vector3 inlet = default;
        /// <summary> 중심 위치 좌표 </summary>
        public Vector3 center = default;
        /// <summary> 출구 위치 좌표 </summary>
        public Vector3 outlet = default;


        protected override void Awake()
        {
            base.Awake();

            inlet = NodePosition;
            outlet = NodePosition;
            center = NodePosition;
        }
    }
}
