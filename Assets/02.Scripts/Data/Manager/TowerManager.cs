using System.Collections.Generic;
using TetrisDefence.Data.Utill;
using TetrisDefence.Game;
using UnityEngine;

namespace TetrisDefence.Data.Manager
{
    public class TowerManager : SingletonMonoBase<TowerManager>
    {
        public List<TowerController> towers = new ();

        public void Register(TowerController tower)
        {
            towers.Add(tower);
        }
    }
}
