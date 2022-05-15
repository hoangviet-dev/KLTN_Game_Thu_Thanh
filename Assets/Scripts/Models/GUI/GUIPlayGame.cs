using Assets.Scripts.Models.Turret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Models.GUI
{
    internal class GUIPlayGame: MonoBehaviour
    {
        [SerializeField] private GameObject infoContainer;
        [SerializeField] private CameraTakeObject CameraViewTurret;
        [SerializeField] private MenuShop menuShop;
        [SerializeField] private UITurretInfo uiTurretInfo;
        [SerializeField] private TextMeshProUGUI textDescription;

        private GraphicRaycaster raycaster;

        private void Start()
        {
            infoContainer.SetActive(false);
            raycaster = gameObject.GetComponent<GraphicRaycaster>();
        }

        public bool IsPointerOverUIObject(Vector2 screenPosition)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = screenPosition;

            GraphicRaycaster uiRaycaster = raycaster;
            List<RaycastResult> results = new List<RaycastResult>();
            uiRaycaster.Raycast(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        public void SetMenuShop(List<Shop.ShopItem> shopItems)
        {
            if (shopItems == null)
            {
                ResetMenuShop();
            } else
            {
                menuShop.SetMenu(shopItems);
            }
        }

        public void ResetMenuShop()
        {
            menuShop.Reset();
            infoContainer.SetActive(false);
        }

        public void SetInfo(Turret.Turret turret)
        {
            CameraViewTurret.TakeObject(turret.GetBaseObject());
            uiTurretInfo.SetInfo(turret.GetInfo());
            textDescription.text = turret.TurretDescription;
            infoContainer.SetActive(true);
        }
    }
}
