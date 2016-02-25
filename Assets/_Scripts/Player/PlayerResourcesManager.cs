using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using UnityEngine;

namespace Assets._Scripts
{
    public class PlayerResourcesManager
    {
        public const float ResourceGainWhileInactive = .001f;

        public bool IsTurn;

        public ResourceManager ResourceManager
        {
            get;
            private set;
        }

        public List<BasicResource> Resources
        {
            get { return ResourceManager.Resources; }
        } 

        public PlayerResourcesManager()
        {
            ResourceManager = new ResourceManager();
        }

        public void Update()
        {
            if (!IsTurn)
                ResourceManager.AddAmountToAll(ResourceGainWhileInactive * Time.deltaTime);
        }


    }
}
