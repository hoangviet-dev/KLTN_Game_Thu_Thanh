using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    internal class PrefabCTL
    {
        private static PrefabCTL _instance = null;
        public static PrefabCTL Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PrefabCTL();
                return _instance;
            }
        }

        private GameObject _platformHover = null;
        public GameObject PlatformHover
        {
            get
            {
                if (_platformHover == null)
                {
                    _platformHover = Resources.Load<GameObject>("Prefabs/PlatformHover");
                }
                return _platformHover;
            }
        }

        private GameObject _platformSelected;
        public GameObject PlatformSelected
        {
            get
            {
                if (_platformSelected == null)
                {
                    _platformSelected = Resources.Load<GameObject>("Prefabs/PlatformSelected");
                }
                return _platformSelected;
            }
        }

        private GameObject _turretPosition;
        public GameObject TurretPosition
        {
            get
            {
                if (_turretPosition == null)
                {
                    _turretPosition = Resources.Load<GameObject>("Prefabs/TurretPosition");
                }
                return _turretPosition;
            }
        }

        private GameObject _menuBuilderItemPrefab;
        public GameObject MenuBuilderItemPrefab
        {
            get
            {
                if (_menuBuilderItemPrefab == null)
                {
                    _menuBuilderItemPrefab = Resources.Load<GameObject>("Prefabs/GUI/UIMenuShopItem");
                }
                return _menuBuilderItemPrefab;
            }
        }

        public GameObject EnemyPrefeb(string name)
        {
            return Resources.Load<GameObject>(String.Format("Prefabs/Enemys/{0}", name));
        }

        public GameObject TurrretReview(string name)
        {
            return Resources.Load<GameObject>(String.Format("Prefabs/Turrets/{0}/Review", name));
        }

        public GameObject Turret(string name)
        {
            return Resources.Load<GameObject>(String.Format("Prefabs/Turrets/{0}/Turret", name));
        }
    }
}
