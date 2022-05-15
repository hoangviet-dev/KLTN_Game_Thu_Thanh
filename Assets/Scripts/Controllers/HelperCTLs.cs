using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class HelperCTLs
    {
        private static HelperCTLs instance;
        public static HelperCTLs Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HelperCTLs();
                }
                return instance;
            }
        }

        public void TakeObject(GameObject _object, Camera camera, Vector3 vectorDirection, float scale = .7f)
        {
            Bounds b = GetBound(_object);
            Vector3 max = b.size;
            float radius = Mathf.Max(max.x, Mathf.Max(max.y, max.z));
            float dist = radius / (Mathf.Sin(camera.fieldOfView * Mathf.Deg2Rad / scale));

            //Vector3 view_direction = transform.InverseTransformDirection(Vector3.back);
            //Vector3 view_direction = UnityEngine.Random.onUnitSphere;
            //Vector3 view_direction = Vector3.back;

            Vector3 pos = vectorDirection * dist + b.center;
            camera.transform.position = pos;
            camera.transform.LookAt(b.center);
        }

        Bounds GetBound(GameObject go)
        {
            Bounds b = new Bounds(go.transform.position, Vector3.zero);
            var rList = go.GetComponentsInChildren(typeof(Renderer));
            foreach (Renderer r in rList)
            {
                b.Encapsulate(r.bounds);
            }
            return b;
        }
    }
}
