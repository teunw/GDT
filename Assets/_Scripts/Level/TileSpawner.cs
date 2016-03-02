using UnityEngine;
using System.Collections;

public class TileSpawner : MonoBehaviour
{

    public const int X = 12;
    public const int Z = 12;

    public GameObject tile;

    void Start () {
        Transform transform = GetComponent<Transform>();
        Vector3 pos = transform.position;

        for (float x = pos.x; x < pos.x + X; x++)
        {
            for (float z = pos.y; z < pos.z + Z; z++)
            {
                GameObject clone = Instantiate(tile, new Vector3(x, pos.y, z), transform.rotation) as GameObject;
                if (clone == null) continue;
                clone.GetComponent<Transform>().parent = transform;
            }
        }
    }
	
	void FixedUpdate ()
	{

	}
}
