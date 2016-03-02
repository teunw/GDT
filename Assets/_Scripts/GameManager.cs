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

    private List<PlayerComponent> Players;

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
            RefreshUI();
        });
        buyCubeButton.onClick.AddListener(() =>
        {
            getPlayerOnTurn.BuyUnit(cubeUnit);
            NextPlayer();
            RefreshUI();
        });
        buySphereButton.onClick.AddListener(() =>
        {
            getPlayerOnTurn.BuyUnit(sphereUnit);
            NextPlayer();
            RefreshUI();
        });
        Players = new List<PlayerComponent>();
    }

    public void RefreshUI()
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

    public void AddPlayer(PlayerComponent player)
    {
        player.setTurn(Players.Count == 0);
        Players.Add(player);
    }

    public PlayerComponent getCurrentPlayer
    {
        get { return Players[playerTurn]; }
    }

    public void AddUnitToUnitContainer(GameObject gameObject)
    {
        gameObject.transform.parent = unitContainer.transform;
    }
   
}
