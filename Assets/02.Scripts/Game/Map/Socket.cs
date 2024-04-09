using System.Collections.Generic;
using UnityEngine;

namespace TetrisDefence.Game.Map
{
    /// <summary>
    /// 맵의 소켓
    /// <br><see cref="MonoBehaviour"/>를 상속 받음</br>
    /// </summary>
    public class Socket : MonoBehaviour
    {
        private List<RoadNode> _roadNodes = default;
        private TowerNode _towerNode = default;


        private void Awake()
        {
            MapOfNodes.Register(this);
            _roadNodes = new List<RoadNode> (GetComponentsInChildren<RoadNode>());
            _towerNode = GetComponentInChildren<TowerNode>();
        }
    }
}
