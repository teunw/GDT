using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
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
        public Color Color;
        public GameObject SelectedGameObject;
        public string PlayerName;

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

            _transform.Translate(Vector3.right*_playerControls.getHorizontal() * Time.deltaTime * SPEED);
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
                clone.GetComponent<Renderer>().material.color = Color;
               

                if (pos != null)
                    clone.GetComponent<Transform>().position = pos;

                GameManager.getInstance().AddUnitToUnitContainer(clone);
                ownedUnits.Add(clone);
                GameManager.getInstance().RefreshUI();
                GameManager.getInstance().AddUnit(clone);
                
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BuyUnit(GameObject obj, Vector3 pos)
        {
            SphereUnit su = obj.GetComponent<SphereUnit>();
            CubeUnit cu = obj.GetComponent<CubeUnit>();
            Unit u;
            if (su != null)
            {
                u = su;
            }
            else if (cu != null)
            {
                u = cu;
            }
            else
            {
                throw new ArgumentException("Could not find unit component");
            }
            u.PlayerComponent = this;
            return BuyUnit(u, obj, pos);
        }

        public bool BuyUnit()
        {
            return BuyUnit(SelectedGameObject, new Vector3());
        }

        public bool BuyUnit(Vector3 pos)
        {
            return BuyUnit(SelectedGameObject, pos);
        }

    }
}