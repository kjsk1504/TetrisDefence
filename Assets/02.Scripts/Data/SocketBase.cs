using UnityEngine;

namespace TetrisDefence.Data
{
    /// <summary>
    /// 맵의 소켓
    /// <br><see cref="MonoBehaviour"/>를 상속 받음</br>
    /// </summary>
    public class SocketBase : MonoBehaviour
    {
        public int[] Location { get; private set; } = new int[2];


        private void Awake()
        {
            int commaIndex = name.IndexOf(',');
            if (int.TryParse(name.Substring(commaIndex - 2, 2), out int location_row))
            {
                if (int.TryParse(name.Substring(commaIndex + 2, 2), out int location_col))
                {
                    Location = new int[2] { location_row, location_col };
                }
            }
            else
            {
                throw new System.Exception($"[Socket]: 소켓({name})은 반드시 (00, 00) 형식으로 끝나야 함");
            }
        }
    }
}
