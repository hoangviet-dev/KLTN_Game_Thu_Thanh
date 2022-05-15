using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class DataCTLs
    {
        private static DataCTLs instance;
        public static DataCTLs Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataCTLs();
                }
                return instance;
            }
        }

        private int level;

        private DataCTLs()
        {
            GetLevel();
        }

        private int GetLevel()
        {
            level = PlayerPrefs.GetInt("Level");
            if (level == 0)
            {
                Level = 1;
            } 
            return level;
        }

        public int Level
        {
            get { return level; }
            set { level = value; PlayerPrefs.SetInt("Level", level); }
        }

    }
}
