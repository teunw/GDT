using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private const int MAX_PROGRESS = 5;

    public GameObject EmptyGameObject;
    public GameObject ProgressGameObject;

    public int Progress;

    void OnDrawGizmos()
    {
        setProgress(Progress);
    }

    public void setProgress(int i)
    {
        Progress = i;
        Transform transform = ProgressGameObject.GetComponent<Transform>();
        if (++i < MAX_PROGRESS)
        {
            Vector3 v = transform.position;
            v.x = i*.08f;
            transform.position = v;

            Vector3 s = transform.localScale;
            s.x = i;
            transform.localScale = s;
        }
    }
}
