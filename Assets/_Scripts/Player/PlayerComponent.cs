using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts
{
    public class PlayerComponent : MonoBehaviour
    {
        public GameObject cubeUnit;

        public PlayerResourcesManager ResourcesManager { get; private set; }

        void Start()
        {
            ResourcesManager = new PlayerResourcesManager();
        }

        void Update()
        {
            ResourcesManager.Update();
        }

        public void buyUnit()
        {
            CubeScript cs = cubeUnit.GetComponent<CubeScript>();
            if (ResourcesManager.ResourceManager.BuyUnit(cs))
            {
                Instantiate(cubeUnit);
            }
            else
            {
                Debug.Log("Nope");
            }
        }
    }
}
