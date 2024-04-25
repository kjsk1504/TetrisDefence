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
        public MinoSocket[,] sockets;

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


        //todo : 기존의 미노 반영

        protected override void Awake()
        {
            base.Awake();

            sockets = new MinoSocket[6, 6];
            Hide();
            onHide += () => tower.towerInfo.OnUpdateTower -= ActivateSockets;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == outPanel.gameObject)
            {
                ResetRegister(); //todo: 기존의 미노 반영
                Hide();
            }
        }

        public override void Show()
        {
            base.Show();

            _towerInfo = new TowerInfo(tower.towerInfo);
            UIUpdate();
            GetTetris();
            ActivateSockets();
            tower.towerInfo.OnUpdateTower += ActivateSockets;
        }

        public void RegisterMino(MinoBase mino)
        {
            _minos.Add(mino);
            _tetris[(int)mino.minoIndex - 1] += 1;
            _towerInfo.UpdateTetris(_tetris);
        }

        public void UnregisterMino(MinoBase mino)
        {
            _minos.Remove(mino);
            mino.Death();
        }

        public void ResetRegister()
        {
            //todo: 기존의 미노의 반영
            var minos = _minos.ToArray();

            foreach (var mino in minos)
            {
                UnregisterMino(mino);
                _tetris[(int)mino.minoIndex - 1] -= 1;
                InventoryManager.Instance.ItemUpdate((int)mino.minoIndex, 1);
            }
            _towerInfo = new (tower.towerInfo);
            
            UIUpdate();
            ActivateSockets();
        }
        
        public void SaveMinos()
        {
            if (VerifingAllTetris())
            {
                tower.towerInfo = _towerInfo;
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
            Hide();
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
            _tetris = _towerInfo.TowerTetris;
            // todo: mino를 _tetris에 저장된 내용에 맞게 배치
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
            foreach (var socket in sockets)
            {
                socket.CheckActivated(_towerInfo.TowerTier);
            }
        }
    }
}
