using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models.Turret
{
    internal class LaserTurret : Turret
    {
        [Header("Attributes")]
        public LineRenderer lineRenderer;
        public Light impactLight;
        public ParticleSystem impactEffect;

        protected override void Init()
        {
            base.Init();
            attackQuantity = 1;
            lineRenderer.enabled = false;
            impactEffect.Stop();
            impactLight.enabled = false;
            info.explosionRadius = 0;
        }

        protected override void SetUpgrade(TurretComponent turretComponent)
        {
            if (turretComponent.GetType() == typeof(TurretComponentLaser))
            {
                TurretComponentLaser turretComponentLaser = (TurretComponentLaser)turretComponent;
                if (turretComponentLaser.lineRenderer != null)
                {
                    Destroy(lineRenderer.gameObject);
                    lineRenderer = turretComponentLaser.lineRenderer;
                    lineRenderer.transform.parent = baseTurret.transform;
                }
                impactLight = turretComponentLaser.impactLight ?? impactLight;


                if (turretComponent.impactEffect != null)
                {
                    Destroy(impactEffect.gameObject);
                    impactEffect = turretComponent.impactEffect;
                    impactEffect.transform.parent = baseTurret.transform;
                }
            }
            base.SetUpgrade(turretComponent);
        }

        protected override void Action()
        {
            base.Action();
            if (target == null)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
                if (attackSound != null && attackSound.isPlaying)
                {
                    attackSound.Stop();
                }
                return;
            }

            if (BaseGameCTLs.Instance.IsEffectSound && attackSound != null && !attackSound.isPlaying)
            {
                attackSound.Play();
            }
            Laser();
        }

        void Laser()
        {
            targetEnemies[0].TakeDamage(damageValue * Time.deltaTime);
            targetEnemies[0].Slow(slowPercent);
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                impactEffect.Play();
                impactLight.enabled = true;
            }
            lineRenderer.SetPosition(0, barrelTransform.position);
            lineRenderer.SetPosition(1, target.position);

            Vector3 dir = barrelTransform.position - target.position;

            impactEffect.transform.position = target.position + dir.normalized * .5f;
            impactEffect.transform.rotation = Quaternion.LookRotation(dir);
        }

        public override TurretInfo GetInfo()
        {
            base.GetInfo();
            info.speed = Mathf.Round((1 / damageValue) * 1000) / 1000;
            return info;
        }

        protected override void Finish()
        {
            base.Finish();
            lineRenderer.enabled = false;
            impactEffect.Stop();
            impactLight.enabled = false;
            if (attackSound != null && attackSound.isPlaying)
            {
                attackSound.Stop();
            }
        }
    }
}
