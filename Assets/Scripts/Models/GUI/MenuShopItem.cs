using Assets.Scripts.Controllers;
using Assets.Scripts.Models.Map;
using Assets.Scripts.Models.Turret;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Assets.Scripts.Models.GUI.GUIPlayGame;

namespace Assets.Scripts.Models.GUI
{
    internal class MenuShopItem : MonoBehaviour
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [SerializeField] private RawImage container;
        [SerializeField] private TextMeshProUGUI textTitle;
        [SerializeField] private TextMeshProUGUI textCost;
        [SerializeField] private Image imgObject;
        [SerializeField] private Button button;
        [SerializeField] private KeyCode code;

        private Shop.ShopItem item;
        private int cost = 0;
        private TooltipTrigger tooltipTrigger;
        private RangeIndicatorTrigger rangeIndicator;

        void Awake()
        {
            Reset();
            tooltipTrigger = gameObject.GetComponent<TooltipTrigger>();
            rangeIndicator = gameObject.GetComponent<RangeIndicatorTrigger>();
        }

        public MenuShopItem SetKeyCode(KeyCode code)
        {
            this.code = code;
            textTitle.text = code.ToString();
            return this;
        }

        public void SetItem(Shop.ShopItem shopItem)
        {
            if (tooltipTrigger != null)
            {
                tooltipTrigger.header = shopItem.Name;
                tooltipTrigger.content = shopItem.Description;
            }

            if (rangeIndicator != null)
            {
                if (Control.GetPlatformTarget() != null)
                {
                    Platform platform = Control.GetPlatformTarget();
                    Turret.Turret turret = platform.GetTurret;
                    rangeIndicator.range = AttributeTurretFloat.ConvertValue(turret.TurretRange, shopItem.Range);
                    rangeIndicator.position = platform.PositionOnPlatform;
                } else
                {
                    rangeIndicator.range = 0;
                }
            }

            textCost.text = String.Format("{0}$", shopItem.Cost);
            cost = shopItem.Cost;
            imgObject.sprite = shopItem.ImageView;
            item = shopItem;
            gameObject.SetActive(true);
        }

        public void Reset()
        {
            textTitle.text = code.ToString();
            textCost.text = "";
            imgObject.sprite = null;
            item = null;
            button.interactable = true;
            cost = 0;
            if (tooltipTrigger != null)
            {
                tooltipTrigger.content = tooltipTrigger.header = "";
            }
            if (rangeIndicator != null)
            {
                rangeIndicator.range = 0;
            }
            gameObject.SetActive(false);
        }

        private bool MoneyIsEnough()
        {
            return cost <= BaseGameCTLs.Instance.Money;
        }

        void Update()
        {
            if (MoneyIsEnough())
            {
                if (!button.interactable)
                {
                    button.interactable = true;
                }

                if (tooltipTrigger.ShowNotEnoughMoney)
                {
                    tooltipTrigger.ShowNotEnoughMoney = false;
                }
            }
            else
            {
                if (button.interactable)
                {
                    button.interactable = false;
                }

                if (!tooltipTrigger.ShowNotEnoughMoney)
                {
                    tooltipTrigger.ShowNotEnoughMoney = true;
                }
            }
        }

        void OnGUI()
        {
            if (code != KeyCode.None)
            {
                if (Event.current.Equals(Event.KeyboardEvent(code.ToString())))
                {
                    OnClick();
                }
            }
        }

        public void OnClick()
        {
            if (code != KeyCode.None)
            {
                Control.MenuItemSelected(item);
            }
        }
    }
}
