using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models.Turret
{
    [Serializable]
    public class AttributeTurretFloat
    {
        public ETypeOfAttributeTurret typeOfAtrribute;
        public float value;
        public AttributeTurretFloat(float value)
        {
            typeOfAtrribute = ETypeOfAttributeTurret.Value;
            this.value = value;
        }
        public static float ConvertValue(float valueOld, AttributeTurretFloat AttributeTurret)
        {
            if (AttributeTurret == null)
            {
                return 0;
            }
            switch (AttributeTurret.typeOfAtrribute)
            {
                case ETypeOfAttributeTurret.None:
                    return valueOld;
                case ETypeOfAttributeTurret.Value:
                    return AttributeTurret.value;
                case ETypeOfAttributeTurret.Add:
                    return valueOld + AttributeTurret.value;
                case ETypeOfAttributeTurret.AddPercent:
                    return valueOld + valueOld * AttributeTurret.value;
                case ETypeOfAttributeTurret.Percent:
                    return valueOld * AttributeTurret.value;
                default:
                    return valueOld;
            }
        }
    }

    [Serializable]
    public class AttributeTurretInt
    {
        public ETypeOfAttributeTurret typeOfAtrribute;
        public int value;
        public AttributeTurretInt(int value)
        {
            typeOfAtrribute = ETypeOfAttributeTurret.Value;
            this.value = value;
        }
        public static int ConvertValue(int valueOld, AttributeTurretInt AttributeTurret)
        {
            if (AttributeTurret == null)
            {
                return 0;
            }
            switch (AttributeTurret.typeOfAtrribute)
            {
                case ETypeOfAttributeTurret.None:
                    return valueOld;
                case ETypeOfAttributeTurret.Value:
                    return AttributeTurret.value;
                case ETypeOfAttributeTurret.Add:
                    return valueOld + AttributeTurret.value;
                case ETypeOfAttributeTurret.AddPercent:
                    return valueOld + valueOld * AttributeTurret.value;
                case ETypeOfAttributeTurret.Percent:
                    return valueOld * AttributeTurret.value;
                default:
                    return valueOld;
            }
        }
    }

    [Serializable]
    public class TurretComponent : MonoBehaviour
    {
        public List<TurretComponent> Childent;

        [Header("Attributes")]
        public int cost;
        public string nameTurret;
        public Sprite imageView;
        public string description;
        public bool isLook = true;
        public AttributeTurretFloat range;
        public AttributeTurretFloat damage;
        public AttributeTurretFloat slowPercent;
        public AttributeTurretFloat explosionRadius;
        public AttributeTurretFloat timeImpact;
        public AttributeTurretFloat fireRate;
        public AttributeTurretInt attackQuantity;
        public Transform customPivot;

        [Header("Object Component")]
        public GameObject headTurret;
        public GameObject footTurret;
        public GameObject bullet;
        public Transform barrelTransform;
        public GameObject startEffect;
        public ParticleSystem impactEffect;


        private void Awake()
        {
            enabled = false;
        }
    }
}
