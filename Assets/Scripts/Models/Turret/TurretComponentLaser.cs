using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models.Turret
{
    public  class TurretComponentLaser: TurretComponent
    {
        [Header("Laser component")]
        public LineRenderer lineRenderer;
        public Light impactLight;
    }
}
