using System.Collections.Generic;
using TetrisDefence.Data.Enums;
using TetrisDefence.Data.Manager;
using TetrisDefence.Game.Map;
using TetrisDefence.Game.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TetrisDefence.UI
{
    public class UITowerInformation : UIScreenBase, IPointerClickHandler
    {
        public Transform minos;
        public Tower tower;

        [SerializeField] private Image outPanel;
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
        private TowerInfo _towerInfo;


        //todo : reset, save, sell, tierup 버튼 구현
        //todo : 미노가 소켓 중 활성화되지 않은 소켓에 닿으면 세이브 버튼 비활성화 및 색으로 알려주기

        protected override void Awake()
        {
            base.Awake();

            Hide();
            onHide += () => tower.towerInfo.OnUpdateTower -= ActivateSockets;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == outPanel.gameObject)
            {
                Hide();
            }
        }

        public override void Show()
        {
            base.Show();

            _towerInfo = tower.towerInfo;
            UIUpdate();
            GetTetris();
            ActivateSockets();
            tower.towerInfo.OnUpdateTower += ActivateSockets;
        }

        public override void Hide()
        {
            base.Hide();

            ResetRegister();
        }

        public void RegisterMino(MinoBase mino)
        {
            _minos.Add(mino);
            AddToTetris(mino.minoIndex);
            _towerInfo.UpdateTetris(_tetris);
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
            if (VerifingAllTetris())
            {
                tower.towerInfo.UpdateTetris(_tetris);
            }
            else
            {
                throw new System.Exception($"[{name}]: tetris가 이상함");
            }
        }

        public void SellTower()
        {
            tower.Death();
            GameManager.Instance.MoneyChange(50);
        }

        public void TowerTierUp()
        {
            _towerInfo.TierUp();
            ActivateSockets();
        }

        private void UIUpdate()
        {
            _no.text = _towerInfo.TowerIndex.ToString();
            _location.text = "(" + string.Join(",", _towerInfo.TowerLocation) + ")";
            _damage.text = _towerInfo.AttackDamage.ToString();
            _speed.text = _towerInfo.AttackSpeed.ToString();
            _slow.text = _towerInfo.SlowTime.ToString();
            _splash.text = _towerInfo.SplashDamage.ToString();
            _dot.text = _towerInfo.DotTime.ToString();
            _cooldown.text = _towerInfo.AttackCooldown.ToString();
            _range.text = _towerInfo.AttackRange.ToString();
        }

        private void GetTetris()
        {
            _tetris = tower.towerInfo.TowerTetris;
            // todo: mino를 _tetris에 맞게 배치
        }

        private void AddToTetris(EMino minoIndex)
        {
            _tetris[(int)minoIndex - 1] += 1;
        }

        private bool VerifingAllTetris()
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
                        return false;
                        throw new System.Exception($"[{name}]: {(EMino)ix}값이 다름");
                    }
                }
            }

            return true;
        }

        private void ActivateSockets()
        {
            var sockets = GetComponentsInChildren<MinoSocket>();

            foreach (var socket in sockets)
            {
                if (socket.Tier <= _towerInfo.TowerTier)
                {
                    socket.Activate();
                }
                else
                {
                    socket.Deactivate();
                }
            }
        }
    }
}
