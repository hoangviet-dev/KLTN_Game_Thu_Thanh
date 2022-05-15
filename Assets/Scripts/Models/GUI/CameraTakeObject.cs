using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models.GUI
{
    public class CameraTakeObject : MonoBehaviour
    {
        public Camera myCamera;
        public Vector3 vectorDirection = Vector3.right;

        public void TakeObject(GameObject _object)
        {
            HelperCTLs.Instance.TakeObject(_object, myCamera, vectorDirection);
        }
    }
}
