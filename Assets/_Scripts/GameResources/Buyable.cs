using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets._Scripts.GameResources
{
    public interface Buyable
    {
        List<BuyRequirement> Requirements { get; }
    }
}