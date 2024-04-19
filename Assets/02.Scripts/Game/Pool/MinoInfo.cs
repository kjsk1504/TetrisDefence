using System;
using TetrisDefence.Data.Enums;
using UnityEngine;

namespace TetrisDefence.Game.Pool
{
    [Serializable]
    [CreateAssetMenu(fileName = "new MinoInfo", menuName = "ScriptableObjects/MinoInfo")]
    public class MinoInfo : ScriptableObject
    {
        public EMino index = EMino.None;
        public int[] block1 = new int[2];
        public int[] block2 = new int[2];
        public int[] block3 = new int[2];
        public int[] block4 = new int[2];
    }
}
