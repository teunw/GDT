using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Random = System.Random;

public class RandomSpawner : MonoBehaviour
{

    public List<RandomBlockable> RandomObjectGameObjects;

    public int BlockedPaths;

    private Random rand = new Random();

	// Use this for initialization
	void Start () {
	    rand = new Random();
	    List<RandomBlockable> blocked = new List<RandomBlockable>();
	    blocked = SelectRandomElements(RandomObjectGameObjects, BlockedPaths);
	    foreach (RandomBlockable blk in RandomObjectGameObjects)
	    {
	        blk.Enabled = blocked.Contains(blk);
	    }

	}

    private List<T> SelectRandomElements<T>(List<T> elements, int select)
    {
        rand = new Random();
        List<T> openElements = new List<T>(elements);
        List<T> selectedElements = new List<T>();
        if (select == 1)
        {
            List<T> t = new List<T>(1);
            t.Add(SelectRandomElement(openElements));
            return t;
        }
        for (int i = 0; i < select; i++)
        {
            T rnd = SelectRandomElement(openElements);
            openElements.Remove(rnd);
            selectedElements.Add(rnd);
        }
        return selectedElements;
    }

    private T SelectRandomElement<T>(List<T> instance)
    {
        if (instance.Count < 2) throw new ArgumentException("Too small list, 0 or 1 items!");
        return instance[rand.Next(0, instance.Count)];
    }
	
	// Update is called once per frame
	void Update () {
	}
}
