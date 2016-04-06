using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using Assets._Scripts;
using Assets._Scripts.GameResources;
using Assets._Scripts.Level;
using Assets._Scripts.Player;
using Assets._Scripts.UI;
using Assets._Scripts.Units;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager getInstance()
    {
        return instance;
    }

    public const float ERROR_TIME = 2f;


    private float ErrorTime = 0f;
    private int playerTurn;
    private ButtonToggleM buttonToggle;
    private AudioSource AudioSource;
    private readonly List<TurnListener> TurnListeners = new List<TurnListener>();

    public readonly List<PlayerComponent> Players = new List<PlayerComponent>();
    public readonly List<Unit> Units = new List<Unit>();
    public readonly List<Tile> Tiles = new List<Tile>();

    public Button EndButton;
    public Button SelectCubeButton;
    public Button SelectSphereButton;
    public Button SelectConverterButton;

    public Text ErrorText;
    public Text WoodText;
    public Text FoodText;
    public Text CoinText;

    public GameObject CubeUnit;
    public GameObject SphereUnit;
    public GameObject ConverterUnit;
    public GameObject UnitContainer;

    public AudioClip ButtonClick;

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


    void OnEnable()
    {
        List<Button> buttons = new List<Button> {SelectCubeButton, SelectSphereButton, SelectConverterButton};
        buttonToggle = new ButtonToggleM(buttons, SelectCubeButton.image.color, Color.blue);
        EndButton.onClick.AddListener(() =>
        {
            if (!GetCurrentPlayer.HadFirstTurn)
            {
                ErrorText.text = "Please make a move before continueing";
                return;
            }
            GoToNextPlayer();
            RefreshUI();
            PlayButtonClick();
        });
           
        SelectSphereButton.onClick.AddListener(() =>
        {
            if (buttonToggle.ToggledButton != SelectSphereButton)
            {
                GetCurrentPlayer.SelectedGameObject = SphereUnit;
                buttonToggle.Toggle(SelectSphereButton);
                PlayButtonClick();
            }
            RefreshUI();
        });
        SelectCubeButton.onClick.AddListener(() =>
        {
            if (buttonToggle.ToggledButton != SelectCubeButton)
            {
                GetCurrentPlayer.SelectedGameObject = CubeUnit;
                buttonToggle.Toggle(SelectCubeButton);
                PlayButtonClick();
            }
            RefreshUI();
        });
        SelectConverterButton.onClick.AddListener(() =>
        {
            if (buttonToggle.ToggledButton != SelectConverterButton)
            {
                GetCurrentPlayer.SelectedGameObject = ConverterUnit;
                buttonToggle.Toggle(SelectConverterButton);
                PlayButtonClick();
            }
            RefreshUI();
        });
    }

    private void PlayButtonClick()
    {
        AudioSource.clip = ButtonClick;
        AudioSource.Play();
    }

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        RefreshUI();
    }

    void Update()
    {
        RefreshUI();
        if (!string.IsNullOrEmpty(ErrorText.text))
        {
            ErrorTime += Time.deltaTime;
            if (ErrorTime > ERROR_TIME)
            {
                ErrorText.text = string.Empty;
                ErrorTime = 0f;
            }
        }
    }

    public void RefreshUI()
    {
        FoodText.text =
            GetCurrentPlayer.ResourcesManager.Resources.Find(o => o.GetType() == typeof (FoodResource)).ToString();
        WoodText.text =
            GetCurrentPlayer.ResourcesManager.Resources.Find(o => o.GetType() == typeof (WoodResource)).ToString();
        CoinText.text =
            GetCurrentPlayer.ResourcesManager.Resources.Find(o => o.GetType() == typeof (CoinResource)).ToString();
        EndButton.enabled = GetCurrentPlayer.HadFirstTurn;
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
        GetCurrentPlayer.HadFirstTurn = true;
        GetCurrentPlayer.SetTurn(false);
        GetCurrentPlayer.ResourcesManager.EndTurn();
        int oldPlayer = playerTurn;
        playerTurn += 1;
        if (playerTurn >= Players.Count)
        {
            playerTurn = 0;
        }
        int newPlayer = playerTurn;
        TurnListeners.FindAll(o => o != null).ForEach(o => o.OnNextTurn(Players[oldPlayer], Players[newPlayer]));
        Players[playerTurn].SetTurn(true);
        buttonToggle.SetToggleColor(GetCurrentPlayer.unitColor);
        RefreshUI();
        return playerTurn;
    }

    public void AddPlayer(PlayerComponent player)
    {
        player.SetTurn(Players.Count == 0);
        Players.Add(player);
    }

    public void AddUnitToUnitContainer(GameObject gameObject)
    {
        gameObject.transform.parent = UnitContainer.transform;
    }

    public bool IsTileWalkable(Vector2 pos)
    {
        return Tiles.Find(o => o.GetGridPosition.Equals(pos)).IsWalkable;
    }

    public bool IsTileWalkable(Vector3 pos)
    {
        return IsTileWalkable(new Vector2(pos.x, pos.z));
    }

    public bool AddUnit(Vector3 pos, Unit unit)
    {
        return AddUnit(new Vector2(pos.x, pos.z), unit);
    }

    public bool AddUnit(Vector2 pos, Unit unit)
    {
        if (IsTileWalkable(pos)) return false;

        unit.Tile = GetTileAtPosition(pos);
        return true;
    }

    public bool AddUnit(GameObject g)
    {
        if (g.GetComponent<Unit>() == null)
        {
            return false;
        }
        return AddUnit(g.GetComponent<Transform>().position, g.GetComponent<Unit>());
    }

    public bool MoveUnit(Vector2 from, Vector2 to, Unit unit)
    {
        if (!IsTileWalkable(from)) return false;
        if (IsTileWalkable(to)) return false;

        unit.Tile = GetTileAtPosition(to);
        return true;
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        return Tiles.Find(o => o.GetGridPosition.Equals(position));
    }

    public void AddTurnListener(TurnListener tl)
    {
        TurnListeners.Add(tl);
    }

    public bool RemoveTurnListener(TurnListener tl)
    {
        return TurnListeners.Remove(tl);
    }
}