using System;
using TetrisDefence.Data.Utill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisDefence.Data.Manager
{
    public class GameManager : SingletonMonoBase<GameManager>
    {
        public TMP_Text _heartText;
        public TMP_Text _coninsText;
        private float _nexusHP = 1000;
        private int _money = 0;


        protected override void Awake()
        {
            base.Awake();

            _heartText = GameObject.Find("Text (TMP) - HP").GetComponent<TMP_Text>();
            _coninsText = GameObject.Find("Text (TMP) - Money").GetComponent<TMP_Text>();
            _heartText.text = Mathf.RoundToInt(_nexusHP).ToString();
            _coninsText.text = Mathf.RoundToInt(_money).ToString();
        }

        public void NexusHPChange(float amount)
        {
            _nexusHP += amount;
            _heartText.text = Mathf.RoundToInt(_nexusHP).ToString();
        }

        public void MoneyChange(int amount)
        {
            _money += amount;
            _coninsText.text = Mathf.RoundToInt(_money).ToString();
        }
    }
}
