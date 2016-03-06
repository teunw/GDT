using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using UnityEngine;

namespace Assets._Scripts.Units
{
    public class SphereUnit : Unit
    {
        public override List<BuyRequirement> Requirements
        {
            get
            {
                return new List<BuyRequirement>()
                {
                    new BuyRequirement(typeof (WoodResource), 2f),
                    new BuyRequirement(typeof (CoinResource), 2f)
                };
            }

        }

        public override string Name
        {
            get { return "Sphere"; }
        }
    }
}
