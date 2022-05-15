using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Models.GUI
{
    internal class UITurretInfoItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;
        private TooltipTrigger tooltipTrigger;

        void Awake()
        {
            enabled = false;
            tooltipTrigger = GetComponent<TooltipTrigger>();
        }

        public void SetValue(string value)
        {
            valueText.text = value;
            if (tooltipTrigger != null)
            {
                tooltipTrigger.content = value;
            }
        }
    }
}
