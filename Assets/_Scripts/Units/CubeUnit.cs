using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Assets._Scripts.GameResources;
using UnityEngine;
using UnityEngine.Networking.Match;

namespace Assets._Scripts.Units
{
    public class CubeUnit : Unit
    {
        public const string parentName = "SpawnedResources";

        public GameObject CoinUnit;
        public GameObject CoinParent;

        public int MaxSpawnCoins = 5 ;
        public float SpawnFrequency = .1f;

        private float time;
        private float coinSpawned;

        public override List<BuyRequirement> Requirements
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

        public override int MovementEnergy
        {
            get { return 2; }
        }

        public override string Name
        {
            get { return "Cube"; }
        }

        public new void Start()
        {
            base.Start();
            GameObject parent = GameObject.Find(parentName);
            if (parent != null)
                CoinParent = parent;
        }

        public new void Update()
        {
            base.Update();
            time += Time.deltaTime;

            if (time > SpawnFrequency && coinSpawned <= MaxSpawnCoins)
            {
                Vector3 pos = GetComponent<Transform>().position;
                pos.y += 2;
                GameObject g = Instantiate(CoinUnit, pos, GetComponent<Transform>().rotation) as GameObject;
                if (g != null && CoinParent != null)
                    g.GetComponent<Transform>().parent = CoinParent.GetComponent<Transform>();
                time = 0;
                coinSpawned++;
            }
        }
    }
}