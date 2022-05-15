using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models.Level
{
    [Serializable]
    public class WaveInfo
    {
        public string Name;
        public float Duration;
        public int Quantity;
        public float Speed;
        public float Health;
        public int WayIndex;
        public bool IsBoss;
    }

    [Serializable]
    public class Wave
    {
        public List<WaveInfo> WaveInfos;
    }

    [Serializable]
    public class LevelData
    {
        public List<Wave> Waves;
    }
}
