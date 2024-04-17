using UnityEngine;

namespace TetrisDefence.Game.Map
{
    /// <summary>
    /// 타워 노드
    /// <br>노드 베이스(<see cref="NodeBase"/>)를 상속받음</br>
    /// </summary>
    public class TowerNode : NodeBase
    {
        /// <summary> 타워 오브젝트가 위에 있는지 여부 </summary>
        public bool OnTower { get; private set; } = default;
        public int[] Location { get; private set; }


        protected override void Awake()
        {
            base.Awake();

            MapOfNodes.Register(this);
            Location = transform.GetComponentInParent<NodeSocket>().Location;
        }
    }
}
