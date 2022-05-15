using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    internal class BaseGameCTLs
    {
        public const string HOME_SCENE = "Home";
        public const string GAME_SCENE = "PlayGame";

        private static BaseGameCTLs instance;
        public static BaseGameCTLs Instance => instance ?? (instance = new BaseGameCTLs());

        private BaseGameCTLs()
        {

        }

        private EGameState _state;
        public EGameState State
        {
            get { return _state; }
            set { _state = value; }
        }

        private int money;
        public int Money
        {
            get { return money; }
            set
            {
                money = value;
            }
        }

        private int health;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        private float speedGame = 1;
        public float SpeedGame
        {
            get { return speedGame; }
            set { speedGame = value; Time.timeScale = value; }
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void ResumeGame()
        {
            Time.timeScale = speedGame;
        }

        private int levelId;
        public int LevelId
        {
            get { return levelId; }
            set { levelId = value; }
        }
        private string mapId;
        public string MapId
        {
            get { return mapId; }
            set { mapId = value; }
        }

        private bool isBackgroundSound = true;
        public bool IsBackgroundSound
        {
            get { return isBackgroundSound; }
            set { isBackgroundSound = value; }
        }

        private bool isEffectSound = true;
        public bool IsEffectSound
        {
            get { return isEffectSound; }
            set { isEffectSound = value; }
        }
    }
}
