using Assets.Scripts.Models.Turret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Models.GUI
{
    internal class UITurretInfo: MonoBehaviour
    {
        [SerializeField] private UITurretInfoItem range;
        [SerializeField] private UITurretInfoItem speed;
        [SerializeField] private UITurretInfoItem damage; 
        [SerializeField] private UITurretInfoItem slowPercent;
        [SerializeField] private UITurretInfoItem explosionRadius;
        [SerializeField] private UITurretInfoItem attackQuantity;

        public void SetInfo(TurretInfo turretInfo)
        {
            range.SetValue(turretInfo.range.ToString());
            range.gameObject.SetActive(turretInfo.range > 0);
            speed.SetValue(turretInfo.speed.ToString());
            speed.gameObject.SetActive(turretInfo.speed > 0);
            damage.SetValue(turretInfo.damage.ToString());
            damage.gameObject.SetActive(turretInfo.damage > 0);
            slowPercent.SetValue(turretInfo.slowPercent.ToString());
            slowPercent.gameObject.SetActive(turretInfo.slowPercent > 0);
            explosionRadius.SetValue(turretInfo.explosionRadius.ToString());
            explosionRadius.gameObject.SetActive(turretInfo.explosionRadius > 0);
            attackQuantity.SetValue(turretInfo.attackQuantity.ToString());
            attackQuantity.gameObject.SetActive(turretInfo.attackQuantity > 0);
        }
    }
}
