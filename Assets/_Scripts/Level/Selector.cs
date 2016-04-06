using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.Misc;
using UnityEngine;

namespace Assets._Scripts.Level
{
    public class Selector : MonoBehaviour
    {
        void Update()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;

                SelectableComponent selectableComponent = objectHit.GetComponent<SelectableComponent>();
                if (selectableComponent == null) return;
                if (selectableComponent.Select())
                {
                    selectableComponent.gameObject.GetComponent<ColorLerpComponent>().Activate();
                    
                }
            }
        }

    }
}
