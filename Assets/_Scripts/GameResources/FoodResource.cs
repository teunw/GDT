using UnityEngine;
using System.Collections;

namespace Assets._Scripts.GameResources
{
    public class FoodResource : BasicResource
    {
        public override string Name
        {
            get { return "Food"; }
        }

        public FoodResource()
        {
        }

        public FoodResource(int startAmount) : base(startAmount)
        {
        }
    }
}