using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.Models.GUI.GUIPlayGame;

namespace Assets.Scripts.Models.GUI
{
    internal class MenuShop : MonoBehaviour
    {
        private class MenuConfig
        {
            public string Title;
            public KeyCode Code;

            public MenuConfig(string title, KeyCode code)
            {
                Title = title;
                Code = code;
            }
        }

        [Serializable]
        private class TurretDefault
        {
            public Turret.Turret TurretTarget;
            public Sprite ImageView;
        }

        [SerializeField] private List<TurretDefault> listTurretDefault;

        private List<Shop.ShopItem> listShopDefault = new List<Shop.ShopItem>();
        //private int size = 60;
        public List<MenuShopItem> items = new List<MenuShopItem>();

        void Awake()
        {
            int indexTurret = 0;
            foreach (TurretDefault turret in listTurretDefault)
            {
                listShopDefault.Add(new Shop.ShopItem(turret.TurretTarget.TurretName, turret.TurretTarget.TurretName, turret.ImageView, turret.TurretTarget.TurretCost, indexTurret++, description: turret.TurretTarget.TurretDescription, range: new Turret.AttributeTurretFloat(turret.TurretTarget.TurretRange)));
            }
        }

        private void Start()
        {
            //transform.position = new Vector3(transform.position.x, size / 2 + 10, 0);
            //DrawMenuBuilder();
            Init();
        }

        void Init()
        {
            SetMenu(listShopDefault);
        }

        //void DrawMenuBuilder()
        //{
        //    MenuConfig[] configs =
        //    {
        //        new MenuConfig("Q", KeyCode.Q),
        //        new MenuConfig("W", KeyCode.W),
        //        new MenuConfig("E", KeyCode.E),
        //        new MenuConfig("R", KeyCode.R),
        //        new MenuConfig("A", KeyCode.A),
        //        new MenuConfig("S", KeyCode.S),
        //        new MenuConfig("D", KeyCode.D),
        //        new MenuConfig("F", KeyCode.F),
        //        new MenuConfig("Space", KeyCode.Space),
        //    };

        //    int positionStart = -(configs.Length-1) * size / 2;

        //    for (int i = 0; i < configs.Length; i++)
        //    {
        //        Vector3 _position = new Vector3(positionStart + i * size, 0, 0);
        //        GameObject _menuBulderItemObject = Instantiate(PrefabCTL.Instance.MenuBuilderItemPrefab, transform);
        //        _menuBulderItemObject.transform.localPosition = _position;
        //        MenuShopItem menuBuilderItem = _menuBulderItemObject.GetComponent<MenuShopItem>();
        //        if (menuBuilderItem != null)
        //        {
        //            menuBuilderItem.SetTitle(configs[i].Title).SetKeyCode(configs[i].Code);
        //            menuBuilderItem.OnSelected = OnSelected;
        //            items.Add(menuBuilderItem);
        //        }

        //    }

        //    SetMenu(listShopDefault);
        //}

        public void SetMenu(List<Shop.ShopItem> shopItems)
        {
            TooltipSystem.Hide();
            if (shopItems == null)
            {
                Reset();
                return;
            }
            for (int i = 0; i < items.Count; i++)
            {
                items[i].Reset();
            }
            foreach (Shop.ShopItem item in shopItems)
            {
                items[(items.Count + item.Index) % items.Count].SetItem(item);
            }
        }

        public void Reset()
        {
            SetMenu(listShopDefault);
        }
    }
}
