﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using UnityEngine;

namespace Assets._Scripts
{
    public class PlayerResourcesManager
    {
        public const float ResourceGain = 0.001f;

        public List<BasicResource> Resources { get; private set; }

        public PlayerResourcesManager()
        {
            Resources = new List<BasicResource>();
            Resources.Add(new WoodResource());
            Resources.Add(new FoodResource());
            Resources.Add(new CoinResource());
        }

        public void AddAmountToAll(float amount)
        {
            Resources.ForEach(o => o.Amount += amount);
        }

        public void EndTurn()
        {
            Resources.ForEach(o => o.OnEndTurn());
        }

        public void Update(bool isTurn)
        {
            if (!isTurn)
                Resources.ForEach(o => o.Amount += ResourceGain * Time.deltaTime);
        }

        public bool BuyUnit(Buyable buyable)
        {
            // Check if buy is possible
            foreach (BuyRequirement b in buyable.Requirements)
            {
                foreach (BasicResource r in Resources)
                {
                    if (b.RequiredResource == r.GetType())
                    {
                        if (b.AmountRequired > r.Amount)
                            return false;
                    }
                }
            }
            // Apply buy
            foreach (BuyRequirement buyRequirement in buyable.Requirements)
            {
                foreach (BasicResource resource in Resources)
                {
                    if (!(buyRequirement.RequiredResource == resource.GetType() &&
                          buyRequirement.AmountRequired < resource.Amount))
                    {
                        resource.Amount -= buyRequirement.AmountRequired;
                    }
                }
            }
            return true;
        }


    }
}
