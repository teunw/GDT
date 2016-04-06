using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.Misc
{
    [RequireComponent(typeof(ColorLerpComponent))]
    public abstract class SelectableComponent : MonoBehaviour
    {
        public abstract bool Select();

    }
}
