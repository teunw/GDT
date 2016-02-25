using UnityEngine;
using System.Collections;

namespace Assets._Scripts.GameResources
{
    public abstract class BasicResource
    {

        public abstract string Name { get; }

        public float Amount { get; set; }

        protected BasicResource()
        {
            Amount = 0;
        }

        public void OnEndTurn()
        {
            Amount += 5;
        }

        public override string ToString()
        {
            return Name + ": " + Amount;
        }
    }
}
