using System.Runtime.CompilerServices;
using UnityEngine;


class MaterialTimeComponent : MonoBehaviour
{

    public const float TIME_LIMIT = 2F;

    public Color OriginalColor;

    private float TimePassed;
    private bool done = false;

    private Renderer renderer;      

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (done)
        {
            renderer.material.color = OriginalColor;
            if (renderer.material.color == OriginalColor)
                Destroy(this);
            return;
        }
        TimePassed += Time.deltaTime;

        if (TimePassed > TIME_LIMIT)
        {
            GetComponent<Renderer>().material.color = OriginalColor;
            done = true;
        }
    }

    public void Reset()
    {
        TimePassed = 0;
    }

}