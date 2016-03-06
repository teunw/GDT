using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Units
{
    public abstract class Unit : MonoBehaviour, Buyable
    {

        public Transform Transform
        {
            get; private set; }

        public PlayerComponent PlayerComponent;

        void Start()
        {
            Transform = GetComponent<Transform>();
        }
       public abstract string Name { get; }

        public abstract List<BuyRequirement> Requirements { get; }

    }
}
