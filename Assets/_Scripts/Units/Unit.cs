using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using UnityEngine;

namespace Assets._Scripts.Units
{
    public interface Unit : Buyable
    {
       string Name { get; }
       
    }
}
