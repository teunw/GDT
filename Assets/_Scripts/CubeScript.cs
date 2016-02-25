using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.GameResources;

public class CubeScript : MonoBehaviour, Buyable {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public List<BuyRequirement> Requirements
    {
        get
        {
            List<BuyRequirement> requirements = new List<BuyRequirement>()
            {
                new BuyRequirement(typeof(WoodResource), 1f),
                new BuyRequirement(typeof(FoodResource), 2f),
                new BuyRequirement(typeof(CoinResource), 3f)
            };
            return requirements;
        }
    }
}
