using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSelector : MonoBehaviour
{

    private Dictionary<Renderer, Color> litObjects;

    // Use this for initialization
    void Start()
    {
        litObjects = new Dictionary<Renderer, Color>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray =
            GameManager.getInstance()
                .getCurrentPlayer.gameObject.GetComponent<Camera>()
                .ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            Color originalColor = objectHit.GetComponent<Renderer>().material.color;
            objectHit.GetComponent<Renderer>().material.color = Color.black;
       
            MaterialTimeComponent mtc = objectHit.gameObject.GetComponent<MaterialTimeComponent>();

            if (mtc == null)
            {
                mtc = objectHit.gameObject.AddComponent<MaterialTimeComponent>();
                mtc.OriginalColor = originalColor;
            }
            else
            {
                mtc.Reset();
            }
        }
    }
}