using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using Assets._Scripts;
using Assets._Scripts.GameResources;
using Assets._Scripts.Player;
using Assets._Scripts.Units;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	public static GameManager getInstance()
	{
	    return instance;
	}

    public List<PlayerComponent> Players { get; private set; }

    private int playerTurn; 

    public Button endButton;
    public Button buyCubeButton;
    public Button buySphereButton;

    public Text woodText;
    public Text foodText;
    public Text coinText;

    public GameObject cubeUnit;
    public GameObject sphereUnit;
    public GameObject unitContainer;

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

    void OnEnable() {
        endButton.onClick.AddListener(() =>
        {
            NextPlayer();
            refreshUI();
        });
        buyCubeButton.onClick.AddListener(() =>
        {
            getPlayerOnTurn.BuyUnit(cubeUnit);
            refreshUI();
        });
        buySphereButton.onClick.AddListener(() =>
        {
            getPlayerOnTurn.BuyUnit(sphereUnit);
            refreshUI();
        });
        Players = new List<PlayerComponent>();
    }

    void Update()
    {
        
    }

    void refreshUI()
    {
        foodText.text = getPlayerOnTurn.ResourcesManager.Resources.Find(o => o.GetType() == typeof(FoodResource)).ToString();
        woodText.text = getPlayerOnTurn.ResourcesManager.Resources.Find(o => o.GetType() == typeof(WoodResource)).ToString();
        coinText.text = getPlayerOnTurn.ResourcesManager.Resources.Find(o => o.GetType() == typeof(CoinResource)).ToString();
    }

    PlayerComponent getPlayerOnTurn
    {
        get { return Players[playerTurn]; }
    }

    int NextPlayer()
    {
        getPlayerOnTurn.setTurn(false);
        getPlayerOnTurn.ResourcesManager.EndTurn();
        playerTurn += 1;
        if (playerTurn >= Players.Count)
        {
            playerTurn = 0;
        }
        Players[playerTurn].setTurn(true);
        return playerTurn;
    }

    public void AddUnitToUnitContainer(GameObject gameObject)
    {
        gameObject.transform.parent = unitContainer.transform;
    }
   
}
