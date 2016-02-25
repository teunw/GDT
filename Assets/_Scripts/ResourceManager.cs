using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.GameResources;

public class ResourceManager
{
    public List<BasicResource> Resources { get; private set; }

    public ResourceManager()
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
