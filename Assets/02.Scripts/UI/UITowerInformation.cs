using System.Collections.Generic;
using TetrisDefence.Data.Enums;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Pool;
using TMPro;
using UnityEngine;

namespace TetrisDefence.UI
{
    public class UITowerInformation : UIScreenBase
    {
        public TowerInfo TowerInfo { get; set; }

        public Transform minos;

        [SerializeField] private TMP_Text _no;
        [SerializeField] private TMP_Text _location;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _slow;
        [SerializeField] private TMP_Text _splash;
        [SerializeField] private TMP_Text _dot;
        [SerializeField] private TMP_Text _cooldown;
        [SerializeField] private TMP_Text _range;
        [SerializeField] private List<MinoBase> _minos = new();
        [SerializeField] private int[] _tetris = new int[7];


        protected override void Awake()
        {
            base.Awake();

            Hide();
        }

        public override void Show()
        {
            base.Show();

            _no.text = TowerInfo.TowerIndex.ToString();
            _location.text = "(" + string.Join(",", TowerInfo.TowerLocation) + ")";
            _damage.text = TowerInfo.AttackDamage.ToString();
            _speed.text = TowerInfo.AttackSpeed.ToString();
            _slow.text = TowerInfo.SlowTime.ToString();
            _splash.text = TowerInfo.SplashDamage.ToString();
            _dot.text = TowerInfo.DotTime.ToString();
            _cooldown.text = TowerInfo.AttackCooldown.ToString();
            _range.text = TowerInfo.AttackRange.ToString();

            GetTetris();
        }

        public override void Hide()
        {
            base.Hide();

            ResetRegister();
        }

        private void GetTetris()
        {
            _tetris = TowerInfo.TowerTetris;
        }

        public void RegisterMino(MinoBase mino)
        {
            _minos.Add(mino);
            AddToTetris(mino.minoIndex);
        }

        public void UnregisterMino(MinoBase mino)
        {
            _minos.Remove(mino);
            mino.Death();
        }

        public void ResetRegister()
        {
            var minos = _minos.ToArray();

            foreach (var mino in minos)
            {
                UnregisterMino(mino);
                InventoryManager.Instance.ItemUpdate((int)mino.minoIndex, 1);
            }
        }

        public void SaveMinos()
        {
            VerifingAllTetris();
            TowerInfo.UpdateTetris(_tetris);
        }

        private void AddToTetris(EMino minoIndex)
        {
            _tetris[(int)minoIndex - 1] += 1;
        }

        private void VerifingAllTetris()
        {
            var tetris = new int[7];

            foreach (var mino in _minos)
            {
                tetris[(int)mino.minoIndex - 1] += 1;
            }

            if (tetris.Equals(_tetris))
            {
                for (int ix = 0; ix < tetris.Length; ix++)
                {
                    if (tetris[ix] != _tetris[ix])
                    {
                        throw new System.Exception($"[{name}]: {(EMino)ix}값이 다름");
                    }
                }
            }
        }
    }
}
