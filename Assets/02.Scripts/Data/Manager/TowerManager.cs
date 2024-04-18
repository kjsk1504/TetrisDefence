using System.Collections.Generic;
using TetrisDefence.Data.Utill;
using TetrisDefence.Game.Pool;
using TetrisDefence.UI;
using UnityEngine;

namespace TetrisDefence.Data.Manager
{
    public class TowerManager : SingletonMonoBase<TowerManager>
    {
        [SerializeField] private List<Tower> _towers = new ();
        private UITowerInformation _towerInfoUI;


        private void Start()
        {
            _towerInfoUI = UIManager.Instance.Get<UITowerInformation>();
        }

        public void Register(Tower tower)
        {
            _towers.Add(tower);
            tower.TowerIndex = _towers.Count;
        }

        public void Unregister(Tower tower)
        {
            int towerindex = _towers.IndexOf(tower);
            if (_towers.Remove(tower))
            {
                for(int ix = towerindex; ix < _towers.Count; ix++)
                {
                    _towers[ix].TowerIndex = ix + 1;
                }
            }
            else
            {
                throw new System.Exception("[TowerManager]: 삭제하려는 타워가 없습니다.");
            }
        }

        public void TowerSelection(TowerInfo selected)
        {
            _towerInfoUI.Hide();
            _towerInfoUI.TowerInfo = selected;
            _towerInfoUI.Show();
        }

        public void ShootAllTowers()
        {
            foreach(var tower in _towers)
            {
                tower.Shoot();
            }
        }
    }
}
