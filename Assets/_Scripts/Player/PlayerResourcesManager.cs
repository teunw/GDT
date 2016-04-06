using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

namespace Assets._Scripts.Player
{
    public class PlayerResourcesManager
    {
        public const int StartResources = 2;
        public const float ResourceGain = .50f;

        private float ResourceGained;
        public List<BasicResource> Resources { get; private set; }

        public PlayerResourcesManager()
        {
            Resources = new List<BasicResource>();
            Resources.Add(new WoodResource(StartResources));
            Resources.Add(new FoodResource(StartResources));
            Resources.Add(new CoinResource(StartResources));
        
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
                ResourceGained += (ResourceGain*Time.deltaTime);
            else
                ResourceGained = 0f;

            if (!(ResourceGained >= 1)) return;
            Resources.ForEach(o => o.Amount += 1);
            ResourceGained = 0;
        }

        public bool BuyUnit(Buyable buyable)
        {
            if (!CanBuyUnit(buyable))
            {
                return false;
            }
            ApplyRequirements(buyable);
            return true;
        }

        private void ApplyRequirements(Buyable buyable)
        {
            foreach (BuyRequirement req in buyable.Requirements)
            {
                BasicResource res = Resources.Find(o => o.GetType() == req.RequiredResource);

                if (res == null) throw new KeyNotFoundException("Resource not found");
                res.Amount -= req.AmountRequired;
            }
        }

        public bool CanBuyUnit(Buyable buyable)
        {
            foreach (BuyRequirement req in buyable.Requirements)
            {
                BasicResource res = Resources.Find(o => o.GetType() == req.RequiredResource);

                if (res == null) throw new KeyNotFoundException("Resource not found");

                if (!(res.Amount >= req.AmountRequired)) return false;
            }
            return true;
        }
    }
}