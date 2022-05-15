using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models.Turret
{
    [Serializable]
    public class TurretInfo
    {
        public float speed;
        public float damage;
        public float slowPercent;
        public float explosionRadius;
        public float range;
        public int attackQuantity;

        public TurretInfo()
        {
            speed = 0;
            damage = 0;
            slowPercent = 0;
            explosionRadius = 0;
            range = 0;
            attackQuantity = 0;
        }
    }
}
