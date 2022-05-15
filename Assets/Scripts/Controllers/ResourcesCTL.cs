using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class ResourcesCTL
    {
        private static ResourcesCTL _instance = null;
        public static ResourcesCTL Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ResourcesCTL();
                return _instance;
            }
        }

        private ResourcesCTL()
        {

        }

        public Sprite GetIcon(string name)
        {
            return Resources.Load<Sprite>(String.Format("Icon/{0}", name));
        }

        private Sprite iconSale;
        public Sprite IconSale
        {
            get
            {
                if (iconSale == null)
                {
                    iconSale = Resources.Load<Sprite>("Icon/Sale");
                }
                return iconSale;
            }
        }
    }
}
