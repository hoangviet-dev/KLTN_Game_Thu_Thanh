using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models.Bullet
{
    public class Bullet : MonoBehaviour
    {
        [Header("Attributes")]
        [HideInInspector] public float explosionRadius = 0f;
        public float speed = 10f;
        [HideInInspector] public float damageValue = 0f;
        [HideInInspector] public float slowPercent = 0f;

        public GameObject impactEffect;
        [HideInInspector] public float impactTime = 2f;

        private Transform target;

        public void Seek(Transform _target)
        {
            target = _target;
        }

        private void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }

        /// <summary>
        /// Ham xu ly danh mot muc tieu
        /// </summary>
        void HitTarget()
        {

            if (explosionRadius > 0f)
            {
                Explode();
            }
            else
            {
                if (impactEffect != null)
                {
                    GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
                    Destroy(effectIns, impactTime);
                }
                Atack(target.gameObject);
            }

            Destroy(gameObject);
        }

        /// <summary>
        /// Ham xu ly no lan toa cua vien dan
        /// </summary>
        void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == target.tag)
                {

                    if (impactEffect != null)
                    {
                        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
                        ImpactEffect _impactEffect = effectIns.GetComponent<ImpactEffect>();
                        if (_impactEffect != null)
                        {
                            _impactEffect.target = collider.GetComponent<Enemy.Enemy>();
                            _impactEffect.damageValue = damageValue * 70 / 100;
                        }
                        Destroy(effectIns, impactTime);
                    }
                    Atack(collider.gameObject);
                }
            }
        }

        /// <summary>
        /// Ham xu ly pha huy mot enemy
        /// </summary>
        /// <param name="enemy"></param>
        void Atack(GameObject enemyObject)
        {
            Enemy.Enemy enemy = enemyObject.GetComponent<Enemy.Enemy>();
            enemy.TakeDamage(damageValue);
            enemy.Slow(slowPercent);
        }

    }
}
