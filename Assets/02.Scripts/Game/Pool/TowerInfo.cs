using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System;
using System.Text;
using static UnityEditor.FilePathAttribute;

namespace TetrisDefence.Game.Pool
{
    [Serializable]
    public class TowerInfo
    {
        public int TowerIndex { get; private set; } = 0;
        public int[] TowerLocation { get; private set; } = new int[2];
        public int TowerTier { get; private set; } = 1;
        public int AttackDamage { get; private set; } = 1;
        public int AttackSpeed { get; private set; } = 5;
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


        public TowerInfo(TowerInfo towerInfo)
        {
            TowerIndex = towerInfo.TowerIndex;
            TowerLocation = towerInfo.TowerLocation;
            TowerTier = towerInfo.TowerTier;
            TowerTetris = towerInfo.TowerTetris;

            UpdateMinos();
        }

        public TowerInfo(int index, int[] location, int tier, int[] tetris, Action onUpdateTower = null)
        {
            if (location.Length != 2 || tetris.Length != 7)
            {
                throw new Exception($"{location}은 반드시 int[2]형태이어야 하고, {tetris}는 반드시 int[7]형태여야 함");
            }

            TowerIndex = index;
            TowerLocation = location;
            TowerTier = tier;
            TowerTetris = tetris;

            if (onUpdateTower != null) OnUpdateTower += onUpdateTower;

            UpdateMinos();
        }

        public TowerInfo(int index = 0, int location_row = 0, int location_col = 0, int tier = 1, int i = 0, int t = 0, int j = 0, int l = 0, int s = 0, int z = 0, int o = 0, Action onUpdateTower = null)
        {
            TowerIndex = index;
            TowerLocation = new int[2] { location_row, location_col };
            TowerTier = tier;
            TowerTetris = new int[7] { i, t, j, l, s, z, o};

            if (onUpdateTower != null) OnUpdateTower += onUpdateTower;

            UpdateMinos();
        }

        
        public string Print()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"TowerIndex: {TowerIndex}");
            stringBuilder.AppendLine($"TowerLocation: {string.Join(",", TowerLocation)}");
            stringBuilder.AppendLine($"TowerTier: {TowerTier}");
            stringBuilder.AppendLine($"AttackDamage: {AttackDamage}");
            stringBuilder.AppendLine($"AttackSpeed: {AttackSpeed}");
            stringBuilder.AppendLine($"SlowTime: {SlowTime}");
            stringBuilder.AppendLine($"SplashDamage: {SplashDamage}");
            stringBuilder.AppendLine($"DotTime: {DotTime}");
            stringBuilder.AppendLine($"AttackCooldown: {AttackCooldown}");
            stringBuilder.AppendLine($"AttackRange: {AttackRange}");
            stringBuilder.AppendLine($"TowerTetris: {string.Join(",", TowerTetris)}");

            return stringBuilder.ToString();
        }

        public void ChangeIndex(int towerIndex)
        {
            TowerIndex = towerIndex;
        }

        public void TierUp()
        {
            TowerTier += 1;
        }

        public void UpdateTetris(int[] tetris)
        {
            if (tetris.Length != 7)
            {
                throw new Exception($"[TowerInfo]: {tetris}는 반드시 int[7]형태여야 함");
            }

            TowerTetris = tetris;
            UpdateMinos();
        }

        private void UpdateMinos()
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
            AttackDamage = 1;
            AttackSpeed = 5;
            SlowTime = 0.0f;
            SplashDamage = 0.0f;
            DotTime = 0.0f;
            AttackCooldown = 1.0f;
            AttackRange = 4.0f;
        }

        private void UpdateTower()
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
    }
}
