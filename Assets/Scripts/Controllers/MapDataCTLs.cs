using Assets.Scripts.Models;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class MapDataCTLs
    {
        private string mapId;
        private MapData map;
        private MapPrefab mPrefab;
        private int rows, columns;

        public string MapId
        {
            get { return mapId; }
            set { mapId = value; loadData(); }
        }
        //public MapData Map
        //{
        //    get { return map; }
        //}
        public int Rows
        {
            get { return rows; }
        }

        public int Columns
        {
            get { return columns; }
        }

        public MapDataCTLs(string mapId)
        {
            this.mapId = mapId;
            loadData();
        }

        private void loadData()
        {
            //Debug.Log(JsonUtility.ToJson(new int[4] { 1, 2, 3, 4 }));
            //Debug.Log(new int[4] { 1, 2, 3, 4 });
            try
            {
                TextAsset text = Resources.Load<TextAsset>(string.Format("Data/Maps/{0}", mapId));
                map = JsonUtility.FromJson<MapData>(text.text);
                rows = map.Data.Count;
                columns = rows != 0 ? map.Data[0].Data.Count : 0;
                mPrefab = new MapPrefab(map.Name);
            }
            catch
            {
                map = null;
                rows = columns = 0;
                mPrefab = null;
            }
        }


        /// <summary>
        /// Ham lay danh sach cac waypoint
        /// </summary>
        /// <param name="indexs">Danh sach chi so diem neo</param>
        /// <returns>Tra ve 1 danh sach bao gom cac diem neo tu danh sach chi so diem neo duoc cung cap</returns>
        private List<Vector3> getWayPoint(List<int> indexs)
        {
            List<Vector3> result = new List<Vector3>();
            for (int i = 0; i < indexs.Count; i++)
            {
                result.Add(map.WayPoint[indexs[i]]);
            }
            return result;
        }

        /// <summary>
        /// Tham chieu ham tao cac doi tuong cho scenes
        /// </summary>
        /// <param name="original">Doi tuong</param>
        /// <param name="position">Vi tri</param>
        /// <param name="rotation">Quay</param>
        /// <returns></returns>
        public delegate Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);

        /// <summary>
        /// Ham thuc hien ve ban do
        /// </summary>
        /// <param name="instantiate">Ham tao cac doi tuong cho scenes</param>
        public void DrawMap(Instantiate instantiate, GameObject mapMiniMap, Transform parent)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (map.Data[i].Data[j] == 1)
                    {
                        instantiate(mPrefab.Platform, new Vector3(i, 0, j), Quaternion.identity, parent);
                        if (mapMiniMap != null)
                        {
                            instantiate(mapMiniMap, new Vector3(i, 0, j), Quaternion.identity, parent);
                        }
                    }
                    else if (map.Data[i].Data[j] == 0)
                    {
                        instantiate(mPrefab.Way, new Vector3(i, 0, j), Quaternion.identity, parent);
                    }
                    else if (map.Data[i].Data[j] > 1)
                    {
                        GameObject prefabView = mPrefab.GetPrefabView(map.Data[i].Data[j]);
                        if (prefabView != null)
                        {
                            instantiate(prefabView, new Vector3(i, 0, j), Quaternion.identity, parent);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ham lay so luong duong di
        /// </summary>
        /// <returns>So luong duong di</returns>
        public int GetNumberWay()
        {
            return map.Ways.Count;
        }

        /// <summary>
        /// Ham lay danh sach cac diem neo theo chi so duong
        /// </summary>
        /// <param name="wayIndex">chi so duong</param>
        /// <returns>Danh sach cac diem neo</returns>
        public List<Vector3> GetWayPoint(int wayIndex)
        {
            if (map.Ways != null && wayIndex < map.Ways.Count)
            {
                return getWayPoint(map.Ways[wayIndex].Way);
            }
            return new List<Vector3>();
        }
    }
}
