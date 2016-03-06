using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using Assets._Scripts;
using Assets._Scripts.GameResources;
using Assets._Scripts.Player;
using Assets._Scripts.UI;
using Assets._Scripts.Units;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	private static GameManager instance;

	public static GameManager getInstance()
	{
	    return instance;
	}

    private List<PlayerComponent> Players;
    private int playerTurn;
    private GameObject tileParent;
    private List<Vector2> UnitPositions = new List<Vector2>();
    private ButtonToggle buttonToggle;

    public Button endButton;
    public Button SelectCubeButton;
    public Button SelectSphereButton;

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
        {

            //if not, set instance to this
            instance = this;
        }

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
    }


    void OnEnable() {
        buttonToggle = new ButtonToggle(SelectCubeButton, SelectSphereButton, SelectCubeButton.image.color, Color.blue);
        endButton.onClick.AddListener(() =>
        {
            GoToNextPlayer();
            RefreshUI();
        });
        SelectCubeButton.onClick.AddListener(() =>
        {
            GetCurrentPlayer.SelectedGameObject = cubeUnit;
            RefreshUI();
        });
        SelectSphereButton.onClick.AddListener(() =>
        {
            if (buttonToggle.ToggledButton != SelectSphereButton)
            {
                GetCurrentPlayer.SelectedGameObject = sphereUnit;
                buttonToggle.Toggle();
            }
            RefreshUI();
        });
        SelectCubeButton.onClick.AddListener(() =>
        {
            if (buttonToggle.ToggledButton != SelectCubeButton)
            {
                GetCurrentPlayer.SelectedGameObject = cubeUnit;
                buttonToggle.Toggle();
            }
            RefreshUI();
        });
        Players = new List<PlayerComponent>();
    }

    void Update()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foodText.text = GetCurrentPlayer.ResourcesManager.Resources.Find(o => o.GetType() == typeof(FoodResource)).ToString();
        woodText.text = GetCurrentPlayer.ResourcesManager.Resources.Find(o => o.GetType() == typeof(WoodResource)).ToString();
        coinText.text = GetCurrentPlayer.ResourcesManager.Resources.Find(o => o.GetType() == typeof(CoinResource)).ToString();
    }

    public PlayerComponent GetPlayer(int i)
    {
        return Players[i];
    }

    public PlayerComponent GetCurrentPlayer
    {
        get { return Players[playerTurn]; }
    }

    public int GoToNextPlayer()
    {
        GetCurrentPlayer.SetTurn(false);
        GetCurrentPlayer.ResourcesManager.EndTurn();
        playerTurn += 1;
        if (playerTurn >= Players.Count)
        {
            playerTurn = 0;
        }
        Players[playerTurn].SetTurn(true);
        buttonToggle.SetToggleColor(GetCurrentPlayer.unitColor);
        return playerTurn;
    }

    public void AddPlayer(PlayerComponent player)
    {
        player.SetTurn(Players.Count == 0);
        Players.Add(player);
    }

    public void AddUnitToUnitContainer(GameObject gameObject)
    {
        gameObject.transform.parent = unitContainer.transform;
    }

    public void RegisterTileParent(GameObject clone)
    {
        tileParent = clone;
    }

    public bool HasUnit(Vector2 pos)
    {
        return UnitPositions.Contains(pos);
    }

    public bool HasUnit(Vector3 pos)
    {
        return HasUnit(new Vector2(pos.x, pos.z));
    }

    public bool AddUnit(Vector3 pos)
    {
        return AddUnit(new Vector2(pos.x, pos.z));
    }

    public bool AddUnit(Vector2 pos)
    {
        if (HasUnit(pos)) return false;

        UnitPositions.Add(pos);
        return true;
    }

    public bool AddUnit(GameObject g)
    {
        return AddUnit(g.GetComponent<Transform>().position);
    }

    public bool MoveUnit(Vector2 from, Vector2 to)
    {
        Vector2 pos = UnitPositions.Find(o => o.Equals(from));

        if (!HasUnit(from)) return false;
        if (HasUnit(to)) return false;

        Vector2 oldPos = new Vector2(pos.x, pos.y);
        UnitPositions.Add(to);
        UnitPositions.Remove(oldPos);
        return true;
    }
}
