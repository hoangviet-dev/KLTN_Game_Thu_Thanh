using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Models
{
    [Serializable]
    public class MapRow
    {
        public List<int> Data;
    }

    [Serializable]
    public class WayRow
    {
        public List<int> Way;
    }

    [Serializable]
    public class MapData
    {
        public string Name;
        public List<MapRow> Data;
        public List<Vector3> WayPoint;
        public List<WayRow> Ways;
    }
}
