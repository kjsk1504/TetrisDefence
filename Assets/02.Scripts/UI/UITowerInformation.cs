using System.Collections.Generic;
using TetrisDefence.Game;
using TetrisDefence.Game.Mino;
using TMPro;
using UnityEngine;

namespace TetrisDefence.UI
{
    public class UITowerInformation : UIScreenBase
    {
        public TowerInfo TowerInfo { get; set; }
        public List<MinoBase> minos;
        // todo: 미노 등록 함수 만들어서 UIItemBase에서 미노를 드래그했을때 등록하게 하기.

        [SerializeField] private TMP_Text _no;
        [SerializeField] private TMP_Text _location;
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _speed;
        [SerializeField] private TMP_Text _slow;
        [SerializeField] private TMP_Text _splash;
        [SerializeField] private TMP_Text _dot;
        [SerializeField] private TMP_Text _cooldown;
        [SerializeField] private TMP_Text _range;


        protected override void Awake()
        {
            base.Awake();

            TowerInfo = new TowerInfo();
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
        }

        public override void Hide()
        {
            base.Hide();

            foreach (var mino in minos)
            {
                mino.Death();
            }
        }
    }
}
