using System;
using Assets._Scripts.Level;
using UnityEngine;

public class RandomBlockable : MonoBehaviour
{
    public Tile[] Tiles;

    private bool _enabled;

    public bool Enabled
    {
        get { return _enabled; }
        set
        {
            foreach (Tile tile in Tiles)
            {
                if (tile != null)
                    tile.EditorWalkable = value;
            }
            gameObject.SetActive(!value);
            _enabled = value;
        }
    }

    void OnDrawGizmos()
    {
        foreach (Tile tile in Tiles)
        {
            tile.GizmoColor = Color.red;
        }
    }

    void OnDrawGizmosSelected()
    {
        foreach (Tile tile in Tiles)
        {
            tile.GizmoColor = Color.yellow;
        }
    }
}