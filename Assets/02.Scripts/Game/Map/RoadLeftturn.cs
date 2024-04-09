using UnityEngine;

namespace TetrisDefence.Game.Map
{
    /// <summary>
    /// 좌회전 도로
    /// <br>도로 노드(<see cref="RoadNode"/>)를 상속받음</br>
    /// </summary>
    public class RoadLeftturn : RoadNode
    {
        protected override void Awake()
        {
            base.Awake();

            inlet.y = NodePosition.y - 1;
            outlet.x = NodePosition.x - 1;
        }
    }
}
