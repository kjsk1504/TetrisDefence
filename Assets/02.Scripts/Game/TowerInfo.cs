using System;

namespace TetrisDefence.Game
{
    [Serializable]
    public class TowerInfo
    {
        public float AttackDamage { get; set; } = 10.0f;
        public int AttackNumber { get; set; } = 1;
        public float SlowTime { get; set; } = 0.0f;
        public float SplashDamage { get; set; } = 0.0f;
        public float DotTime { get; set; } = 0.0f;
        public float AttackSpeed { get; set; } = 1.0f;
        public float AttackRange { get; set; } = 4.0f;

        private int _minoI = default;
        private int _minoT = default;
        private int _minoJ = default;
        private int _minoL = default;
        private int _minoS = default;
        private int _minoZ = default;
        private int _minoO = default;

        public void UpdateTower()
        {
            AttackDamage *= _minoI;
            AttackNumber += _minoT;
            SlowTime += _minoJ;
            SplashDamage += (float)_minoL / 10;
            DotTime += _minoS;
            AttackSpeed += _minoZ;
            AttackRange *= _minoO;
        }
        
    }
}
