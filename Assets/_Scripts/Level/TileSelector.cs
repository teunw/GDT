using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets._Scripts.Level;

public class TileSelector : MonoBehaviour
{
    public const int FLOOR_LAYER = 8;

    private Color SelectColor;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(2) || !Cursor.visible) return;
        RaycastHit hit;
        Ray ray =
            GameManager.getInstance()
                .GetCurrentPlayer.gameObject.GetComponent<Camera>()
                .ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;

            if (objectHit.gameObject.layer != FLOOR_LAYER) return;
            if (objectHit.GetComponent<Tile>().GetCurrentPlayerComponent == GameManager.getInstance().GetCurrentPlayer) return;

            SelectColor = GameManager.getInstance().GetCurrentPlayer.unitColor;
            Color originalColor = objectHit.GetComponent<Renderer>().material.color;
            objectHit.GetComponent<Renderer>().material.color = SelectColor;
       
            MaterialColorLerper mtc = objectHit.gameObject.GetComponent<MaterialColorLerper>();

            if (mtc == null)
            {
                mtc = objectHit.gameObject.AddComponent<MaterialColorLerper>();
                mtc.OriginalColor = originalColor;
                mtc.CurrentColor = SelectColor;
            }
            else
            {
                mtc.Reset();
            }
        }
    }
}