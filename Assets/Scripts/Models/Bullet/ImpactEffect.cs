using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models.Bullet
{
    public class ImpactEffect : MonoBehaviour
    {
        [HideInInspector] public Enemy.Enemy target;
        [HideInInspector] public float damageValue = 0;

        public bool isDamage = true;
        public float slowPercent = 0;

        void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }
            transform.position = target.transform.position;
            Attack();
        }

        void Attack()
        {
            if (isDamage)
            {
                target.TakeDamage(damageValue * Time.deltaTime);
            }
            target.Slow(slowPercent);
        }
    }
}
