using System;
using TetrisDefence.Data.Utill;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisDefence.Data.Manager
{
    public class GameManager : SingletonMonoBase<GameManager>
    {
        public Button quitButton;
        public TMP_Text heartText;
        public TMP_Text coninsText;
        private float _nexusHP = 1000;
        private int _money = 0;


        protected override void Awake()
        {
            base.Awake();

            quitButton = GameObject.Find("Button - GameQuit").GetComponent<Button>();
            heartText = GameObject.Find("Text (TMP) - HP").GetComponent<TMP_Text>();
            coninsText = GameObject.Find("Text (TMP) - Money").GetComponent<TMP_Text>();
            quitButton.onClick.AddListener(GameExit);
            heartText.text = Mathf.RoundToInt(_nexusHP).ToString();
            coninsText.text = Mathf.RoundToInt(_money).ToString();
        }

        public void GameExit()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
        }

        public void NexusHPChange(float amount)
        {
            _nexusHP += amount;
            heartText.text = Mathf.RoundToInt(_nexusHP).ToString();
        }

        public void MoneyChange(int amount)
        {
            _money += amount;
            coninsText.text = Mathf.RoundToInt(_money).ToString();
        }
    }
}
