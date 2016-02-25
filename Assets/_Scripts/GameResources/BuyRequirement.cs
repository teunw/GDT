using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts.GameResources
{
    public class BuyRequirement
    {

        public Type RequiredResource
        {
            get; private set; }

        public float AmountRequired
        {
            get; private set; }

        public BuyRequirement(Type requiredResource, float amountRequired)
        {
            RequiredResource = requiredResource;
            AmountRequired = amountRequired;
        }
    }
}
