using Assets.Scripts.Models.Level;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public struct WaveDetail
    {
        public WaveInfo waveInfo;
        public GameObject prefab;
    }

    public class LevelCTLs
    {
        private int levelId;
        private LevelData levelData;

        public int WaveCount
        {
            get { return levelData == null ? 0 : levelData.Waves.Count; }
        }

        public int LevelId
        {
            get { return levelId; }
            set { levelId = value; LoadData(); }
        }

        public LevelCTLs(int levelId)
        {
            LevelId = levelId;

        }

        private void LoadData()
        {
            try
            {
                TextAsset text = Resources.Load<TextAsset>(string.Format("Data/Level/{0}", levelId));
                levelData = JsonUtility.FromJson<LevelData>(text.text);
            }
            catch
            {
                levelData = null;
            }
        }

        public List<WaveDetail> GetWave(int waveNumber)
        {
            List<WaveDetail> waves = new List<WaveDetail>();
            if (waveNumber < WaveCount)
            {
                Wave wave = levelData.Waves[waveNumber];
                if (wave != null)
                {
                    foreach (WaveInfo waveInfo in wave.WaveInfos)
                    {
                        WaveDetail waveDetail = new WaveDetail();
                        waveDetail.waveInfo = waveInfo;
                        waveDetail.prefab = PrefabCTL.Instance.EnemyPrefeb(waveInfo.Name);
                        waves.Add(waveDetail);
                    }
                }
            }
            return waves;
        }
    }
}
