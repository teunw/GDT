using UnityEngine;
using System.Collections;
using Assets._Scripts.GameResources;

public class CoinClickable : MonoBehaviour
{
    public void OnMouseDown()
    {
        GameManager.getInstance()
            .GetCurrentPlayer.ResourcesManager.Resources.Find(o => o.GetType() == typeof (CoinResource))
            .Amount += 1;
        GameManager.getInstance().RefreshUI();
        Destroy(gameObject);
    }
}