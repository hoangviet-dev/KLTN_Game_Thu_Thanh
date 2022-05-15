using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models.Turret
{
    internal class ShootTurret : Turret
    {

        [Header("Attributes Shoot")]
        public float fireRate = 0f;
        [SerializeField] protected float explosionRadius;
        [SerializeField] protected GameObject bullet;
        [SerializeField] protected float timeImpact;

        protected float fireCountDown = 0f;

        protected override void Init()
        {
            base.Init();
        }
        protected override void SetUpgrade(TurretComponent turretComponent)
        {
            base.SetUpgrade(turretComponent);
            if (turretComponent.bullet != null)
            {
                bullet = turretComponent.bullet;
            }
            timeImpact = AttributeTurretFloat.ConvertValue(timeImpact, turretComponent.timeImpact);
            explosionRadius = AttributeTurretFloat.ConvertValue(explosionRadius, turretComponent.explosionRadius);
            fireRate = AttributeTurretFloat.ConvertValue(fireRate, turretComponent.fireRate);
        }

        protected override void Action()
        {
            if (target == null)
            {
                return;
            }
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }

        void Shoot()
        {
            foreach (Enemy.Enemy enemy in targetEnemies)
            {
                GameObject _objectBullet = Instantiate(bullet, barrelTransform.position, Quaternion.identity);
                Bullet.Bullet _bullet = _objectBullet.GetComponent<Bullet.Bullet>();
                if (_bullet != null)
                {
                    if (BaseGameCTLs.Instance.IsEffectSound && attackSound != null)
                    {
                        attackSound.PlayOneShot(attackSound.clip);
                    }
                    _bullet.damageValue = damageValue;
                    _bullet.explosionRadius = explosionRadius;
                    _bullet.impactTime = timeImpact;
                    _bullet.Seek(enemy.transform);
                }
                else
                {
                    Destroy(_objectBullet);
                    Debug.Log("Not instantiate Bullet");
                }
            }
        }

        public override TurretInfo GetInfo()
        {
            base.GetInfo();
            info.speed = 1 / fireRate;
            info.explosionRadius = explosionRadius;
            return info;
        }
    }
}
