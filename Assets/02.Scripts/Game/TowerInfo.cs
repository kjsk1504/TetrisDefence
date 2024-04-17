using System;
using System.Numerics;

namespace TetrisDefence.Game
{
    [Serializable]
    public class TowerInfo
    {
        public int TowerIndex { get; private set; } = 0;
        public int[] TowerLocation { get; private set; } = new int[2];
        public int TowerTier { get; private set; } = 1;
        public float AttackDamage { get; private set; } = 10.0f;
        public int AttackSpeed { get; private set; } = 2;
        public float SlowTime { get; private set; } = 0.0f;
        public float SplashDamage { get; private set; } = 0.0f;
        public float DotTime { get; private set; } = 0.0f;
        public float AttackCooldown { get; private set; } = 1.0f;
        public float AttackRange { get; private set; } = 4.0f;
        public int[] TowerTetris { get; private set; } = new int[7];

        public event Action OnUpdateTower;

        private int _minoI = default;
        private int _minoT = default;
        private int _minoJ = default;
        private int _minoL = default;
        private int _minoS = default;
        private int _minoZ = default;
        private int _minoO = default;


        public TowerInfo(int index, int[] location, int tier, int[] tetris)
        {
            TowerIndex = index;
            TowerLocation = location;
            TowerTier = tier;
            TowerTetris = tetris;

            UpdateMinos();
        }

        public TowerInfo(int index = 0, int location_row = 0, int location_col = 0, int tier = 1, int i = 0, int t = 0, int j = 0, int l = 0, int s = 0, int z = 0, int o = 0)
        {
            TowerIndex = index;
            TowerLocation = new int[2] { location_row, location_col };
            TowerTier = tier;
            TowerTetris = new int[7] { i, t, j, l, s, z, o};

            UpdateMinos();
        }

        public void UpdateTower()
        {
            AttackDamage *= (_minoI + 1);
            AttackSpeed *= (_minoT + 1);
            SlowTime += _minoJ;
            SplashDamage += (float)_minoL / 10;
            DotTime += _minoS;
            AttackCooldown /= (_minoZ + 1);
            AttackRange *= (_minoO + 1);

            OnUpdateTower?.Invoke();
        }

        public void UpdateMinos()
        {
            _minoI = TowerTetris[0];
            _minoT = TowerTetris[1];
            _minoJ = TowerTetris[2];
            _minoL = TowerTetris[3];
            _minoS = TowerTetris[4];
            _minoZ = TowerTetris[5];
            _minoO = TowerTetris[6];

            InitializeTower();
            UpdateTower();
        }

        private void InitializeTower()
        {
            AttackDamage = 10.0f;
            AttackSpeed = 5;
            SlowTime = 0.0f;
            SplashDamage = 0.0f;
            DotTime = 0.0f;
            AttackCooldown = 1.0f;
            AttackRange = 4.0f;
        }
    }
}
