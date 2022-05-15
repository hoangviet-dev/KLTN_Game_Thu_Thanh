using Assets.Scripts.Models.Turret;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models.Shop
{
    public class ShopItem
    {
        public string Name { get; set; }
        public Sprite ImageView { get; set; }
        public string Value { get; set; }
        public int Cost { get; set; }
        public int Index { get; set; }

        public AttributeTurretFloat Range { get; set; }

        public string Description { get; set; }

        public ShopItem(string name, string value, Sprite imageView, int cost, int index = 0, string description = "", AttributeTurretFloat range = null)
        {
            Name = name;
            Value = value;
            Cost = cost;
            ImageView = imageView;
            Index = index;
            Description = description;
            Range = range;
        }
    }
}
