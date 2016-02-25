using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets._Scripts;
using Assets._Scripts.GameResources;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	public static GameManager getInstance()
	{
	    return instance;
	}

    public List<PlayerComponent> Players
    {
        get; private set; }

    private int playerTurn;

    public Button endButton;

    public Text woodText;
    public Text foodText;
    public Text coinText;

    public GameObject cube;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
    }

    void Start() {
        Players.Add(new PlayerComponent());
        Players.Add(new PlayerComponent());
        endButton.onClick.AddListener(() =>
        {
            NextPlayer();
            refreshUI();
        });
    }

    void Update()
    {
        
    }

    void refreshUI()
    {
        foodText.text = getPlayerOnTurn().ResourcesManager.Resources.Find(o => o.GetType() == typeof(FoodResource)).ToString();
        woodText.text = getPlayerOnTurn().ResourcesManager.Resources.Find(o => o.GetType() == typeof(WoodResource)).ToString();
        coinText.text = getPlayerOnTurn().ResourcesManager.Resources.Find(o => o.GetType() == typeof(CoinResource)).ToString();
    }

    PlayerComponent getPlayerOnTurn()
    {
        return Players[playerTurn];
    }

    int NextPlayer()
    {
        ++playerTurn;
        if (playerTurn > Players.Count)
        {
            playerTurn = 0;
        }
        return playerTurn;
    }
   
}
