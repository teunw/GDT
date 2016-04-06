using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using Assets._Scripts.Level;

public class TileSpawner : MonoBehaviour
{
    public int X = 16;
    public int Z = 16;

    public GameObject tile;
    public bool active;

    void Start()
    {
        if (!active) return;
        Transform transform = GetComponent<Transform>();
        Vector3 pos = transform.position;

        int halfX = X/2;
        int halfZ = Z/2;
        for (int x = (int) pos.x - halfX; x <= pos.x + X - halfX; x++)
        {
            int max = (int) pos.z + Z - halfZ;
            int min = (int) pos.z - halfZ;
            for (int z = min; z <= max; z++)
            {
                GameObject clone = Instantiate(tile, new Vector3(x, pos.y, z), transform.rotation) as GameObject;
                if (clone == null) continue;
                clone.GetComponent<Transform>().parent = transform;

                Tile tileComponent = clone.GetComponent<Tile>();
                if (IsHalfWay(z, min, max))
                {
                    tileComponent.PlayerComponent = 0;
                }
                else
                {
                    tileComponent.PlayerComponent = 1;
                }
            }
        }
    }

    private bool IsHalfWay(float z, float min, float max)
    {
        float avg = (min + max)/2;
        return z >= avg;
    }

    void FixedUpdate()
    {
    }
}