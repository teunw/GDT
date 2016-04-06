using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using Assets._Scripts.Level;
using Assets._Scripts.Misc;
using Assets._Scripts.Player;
using Assets._Scripts.Units;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        public const float SPEED = 36f;

        private Camera _camera;
        private PlayerControls _playerControls;
        private bool IsTurn;
        private Transform _transform;
        private List<GameObject> ownedUnits;

        public bool startOnTurn;
        public Color unitColor;
        public GameObject SelectedGameObject;

        [HideInInspector]
        public Unit SelectedUnit;
        [HideInInspector]
        public bool HadFirstTurn;

        public PlayerResourcesManager ResourcesManager { get; private set; }

        void Start()
        {
            ResourcesManager = new PlayerResourcesManager();
            _playerControls = new PlayerControls();
            _transform = GetComponent<Transform>();
            ownedUnits = new List<GameObject>();

            _camera = GetComponent<Camera>();

            GameManager.getInstance().AddPlayer(this);
        }

        void Update()
        {
            ResourcesManager.Update(IsTurn);

            _transform.Translate(Vector3.right*_playerControls.getHorizontal()*Time.deltaTime*SPEED);
            _transform.Translate(Vector3.forward*_playerControls.getVertical()*Time.deltaTime*SPEED);
            _playerControls.UpdateCamera(_transform);
            _playerControls.UpdateZoom(_transform);
        }

        public void SetTurn(bool turn)
        {
            IsTurn = turn;
            gameObject.GetComponent<Camera>().enabled = turn;
        }

        private bool BuyUnit(Buyable unit, GameObject obj, Vector3 pos)
        {
            if (ResourcesManager.BuyUnit(unit))
            {
                GameObject clone = Instantiate(obj);
                clone.GetComponent<Renderer>().material.color = unitColor;


                if (pos != null)
                    clone.GetComponent<Transform>().position = pos;

                GameManager.getInstance().AddUnitToUnitContainer(clone);
                GameManager.getInstance().RefreshUI();
                GameManager.getInstance().AddUnit(clone);
                ownedUnits.Add(clone);

                return true;
            }
            return false;
        }

        public bool BuyUnit(GameObject obj, Vector3 pos, Tile ownerTile)
        {
            SphereUnit su = obj.GetComponent<SphereUnit>();
            CubeUnit cu = obj.GetComponent<CubeUnit>();
            ConverterUnit conU = obj.GetComponent<ConverterUnit>();
            Unit u;
            if (su != null)
            {
                u = su;
            }
            else if (cu != null)
            {
                u = cu;
            }
            else if (conU != null)
            {
                u = conU;
            }
            else
            {
                throw new ArgumentException("Could not find unit component");
            }
            ColorLerpComponent clc = u.gameObject.GetComponent<ColorLerpComponent>();
            clc.SelectColor = Color.yellow;
            u.PlayerComponent = this;
            u.Tile = ownerTile;
            return BuyUnit(u, obj, pos);
        }

        public bool BuyUnit(Vector3 pos, Tile ownerTile)
        {
            return BuyUnit(SelectedGameObject, pos, ownerTile);
        }
    }
}