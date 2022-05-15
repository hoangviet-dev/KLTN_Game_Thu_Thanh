using Assets.Scripts.Controllers;
using Assets.Scripts.Models.Turret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Models.Map
{
    public class Platform : MonoBehaviour
    {
        private GameObject objectTurret;
        private Turret.Turret turret;
        private Vector3 _PositionOnPlatform;

        public bool HaveTurret
        {
            get { return turret != null; }
        }

        public Turret.Turret GetTurret
        {
            get { return turret; }
        }

        private void Init()
        {
            Vector3 _tranform = Vector3.zero;
            _PositionOnPlatform = transform.position;
            _PositionOnPlatform.y += GetComponent<Renderer>().bounds.size.y / 2 + .01f;
            _tranform.y += GetComponent<Renderer>().bounds.size.y / 2 + .01f;
        }

        private void Start()
        {
            Init();
        }

        public List<Shop.ShopItem> GetListShop()
        {
            if (turret != null)
            {
                return turret.GetListShop();
            }
            return null;
        }

        public EBuildTurretState BuildTurret(Shop.ShopItem item)
        {
            if (item != null && BaseGameCTLs.Instance.Money >= item.Cost)
            {
                BaseGameCTLs.Instance.Money -= item.Cost;
                if (objectTurret == null)
                {
                    GameObject _object = PrefabCTL.Instance.Turret(item.Value);
                    objectTurret = Instantiate(_object, _PositionOnPlatform, Quaternion.identity);
                    turret = objectTurret.GetComponent<Turret.Turret>();
                    turret.Build();
                }
                else
                {
                    return turret.Upgrade(item);
                }

                return EBuildTurretState.Success;
            }
            else
            {
                return EBuildTurretState.False;
            }
        }

        public Vector3 PositionOnPlatform
        {
            get
            {
                return _PositionOnPlatform;
            }
        }

        public void ShowRangeIndicator()
        {
            RangeIndicatorSystem.ShowTarget(turret.TurretRange, PositionOnPlatform);
        }

        public void ShowRangeIndicator(float range)
        {
            RangeIndicatorSystem.ShowTarget(range, PositionOnPlatform);
        }
    }
}
