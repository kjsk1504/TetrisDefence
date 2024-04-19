using System.Collections.Generic;
using TetrisDefence.Data.Utill;
using TetrisDefence.Game.Pool;
using TetrisDefence.UI;

namespace TetrisDefence.Data.Manager
{
    public class TowerManager : SingletonMonoBase<TowerManager>
    {
        public bool isUIActive;
        public Tower selectedTower;
        public UITowerInformation towerInfoUI;
        public List<Tower> towers = new ();


        private void Start()
        {
            towerInfoUI = UIManager.Instance.Get<UITowerInformation>();
            towerInfoUI.onShow += () => isUIActive = true;
            towerInfoUI.onHide += () => isUIActive = false;
        }

        public void Register(Tower tower)
        {
            towers.Add(tower);
            tower.towerIndex = towers.Count;
        }

        public void Unregister(Tower tower)
        {
            int towerindex = towers.IndexOf(tower);
            if (towers.Remove(tower))
            {
                for(int ix = towerindex; ix < towers.Count; ix++)
                {
                    towers[ix].towerIndex = ix + 1;
                }
            }
            else
            {
                throw new System.Exception("[TowerManager]: 삭제하려는 타워가 없습니다.");
            }
        }

        public void Unregister(int towerIndex)
        {
            Unregister(towers[towerIndex]);
        }

        public void TowerSelection(Tower selected)
        {
            towerInfoUI.Hide();
            selectedTower = selected;
            towerInfoUI.tower = selectedTower;
            towerInfoUI.Show();
        }
    }
}
