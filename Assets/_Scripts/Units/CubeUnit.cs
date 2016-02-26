using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using UnityEngine;
using UnityEngine.Networking.Match;

namespace Assets._Scripts.Units
{
    class CubeUnit : MonoBehaviour, Unit
    {
        public List<BuyRequirement> Requirements
        {
            get
            {
                return new List<BuyRequirement>()
                {
                    new BuyRequirement(typeof (CoinResource), 2f),
                    new BuyRequirement(typeof (WoodResource), 2f)
                };
            }
        }

        public string Name
        {
            get { return "Cube"; }
        }
    }
}
