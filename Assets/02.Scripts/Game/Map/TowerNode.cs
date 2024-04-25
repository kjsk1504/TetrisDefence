using TetrisDefence.Game.Pool;
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
        public bool OnTower { get; private set; } = false;
        public int[] Location { get; private set; } = new int[2];
        public Tower Tower { get; private set; } = null;


        protected override void Awake()
        {
            base.Awake();

            MapOfNodes.Register(this);
            Location = transform.GetComponentInParent<NodeSocket>().Location;
        }

        public void Mount(Tower tower)
        {
            Tower = tower;
            OnTower = true;
        }

        public bool Unmount()
        {
            if (Tower != null)
            {
                Tower = null;
                OnTower = false;
                return true;
            }

            print("해제할 타워가 없음");
            return false;
        }
    }
}
