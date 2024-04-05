using System.Collections.Generic;
using System;

namespace TetrisDefence.Game.Map
{
    /// <summary>
    /// 노드(<see cref="NodeBase"/>)와 소켓(<see cref="Socket"/>)을 등록한 맵의 정보
    /// </summary>
    public static class MapOfNodes
    {
        /// <summary>  </summary>
        public static List<RoadNode> roads = new();
        /// <summary>  </summary>
        public static List<TowerNode> towers = new();
        /// <summary>  </summary>
        public static Socket[,] sockets = new Socket[12, 12];


        /// <summary>
        /// 노드 베이스(<see cref="NodeBase"/>)를 등록하는 함수
        /// </summary>
        /// <param name="node"> 등록할 노드베이스 (<see cref="RoadNode"/>, <see cref="TowerNode"/>) </param>
        public static void Register(NodeBase node)
        {
            if (node is RoadNode)
            {
                roads.Add((RoadNode)node);
            }
            else if (node is TowerNode)
            {
                towers.Add((TowerNode)node);
            }
        }

        /// <summary>
        /// 소켓(<see cref="Socket"/>)을 등록하는 함수
        /// </summary>
        /// <param name="socket"> 등록할 소켓 <br>소켓의 게임 오브젝트는 이름이 (00, 00)의 형식으로 끝나야함</br> </param>
        public static void Register(Socket socket)
        {
            string rowString = string.Join("", socket.name[socket.name.Length - 7], socket.name[socket.name.Length - 6]);
            string colString = string.Join("", socket.name[socket.name.Length - 3], socket.name[socket.name.Length - 2]);

            if (int.TryParse(rowString, out int row) && int.TryParse(colString, out int col))
            {
                sockets[row - 1, col - 1] = socket;
            }
        }
    }
}
