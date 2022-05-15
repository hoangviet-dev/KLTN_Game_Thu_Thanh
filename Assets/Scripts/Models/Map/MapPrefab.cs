using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    internal class MapPrefab
    {
        private string name;
        private GameObject platform;
        private GameObject way;
        private List<GameObject> prefabListView;
        private GameObject defaultObject;

        public string Name
        {
            get { return name; }
            set { name = value; loadPrefab(); }
        }

        public GameObject Platform
        {
            get { return platform; }
        }

        public GameObject Way
        {
            get { return way; }
        }

        public MapPrefab(string name)
        {
            this.name = name;
            loadPrefab();
        }

        private void loadPrefab()
        {
            platform = Resources.Load<GameObject>(String.Format("Prefabs/Maps/{0}/Platform", name));
            way = Resources.Load<GameObject>(String.Format("Prefabs/Maps/{0}/Way", name));
            defaultObject = Resources.Load<GameObject>(String.Format("Prefabs/Maps/{0}/Default", name));

            int countPrefabView = 2;
            prefabListView = new List<GameObject>();
            while (true)
            {
                GameObject prefabView = Resources.Load<GameObject>(String.Format("Prefabs/Maps/{0}/{1}", name, countPrefabView));
                if (prefabView == null)
                {
                    break;
                }
                prefabListView.Add(prefabView);
                countPrefabView++;
            }
        }

        public GameObject GetPrefabView(int index)
        {
            if (index-2 >= prefabListView.Count)
            {
                return defaultObject;
            }
            return prefabListView[index-2];
        }
    }
}
