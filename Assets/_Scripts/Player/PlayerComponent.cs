using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using Assets._Scripts.Player;
using Assets._Scripts.Units;
using UnityEngine;

namespace Assets._Scripts.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        private Camera _camera;
        private PlayerControls _playerControls;
        private bool IsTurn;
        private Dictionary<Unit, GameObject> unitGameObjects;
        private Transform _transform;

        public bool startOnTurn;
        public GameObject cubeUnit;
        public Color unitColor;

        public PlayerResourcesManager ResourcesManager { get; private set; }

        void Start()
        {
            ResourcesManager = new PlayerResourcesManager();
            _playerControls = new PlayerControls();
            _transform = GetComponent<Transform>();

            _camera = GetComponent<Camera>();

            GameManager.getInstance().AddPlayer(this);
        }

        void Update()
        {
            ResourcesManager.Update(IsTurn);

            _transform.Translate(Vector3.right*_playerControls.getHorizontal());
            _transform.Translate(Vector3.forward*_playerControls.getVertical());
        }

        public void setTurn(bool turn)
        {
            IsTurn = turn;
            gameObject.GetComponent<Camera>().enabled = turn;
        } 

        private bool BuyUnit(Buyable unit, GameObject obj)
        {
            if (ResourcesManager.BuyUnit(unit))
            {
                GameObject clone = Instantiate(obj);
                clone.GetComponent<Renderer>().material.color = unitColor;
                GameManager.getInstance().AddUnitToUnitContainer(clone);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool BuyUnit(GameObject obj)
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
            return BuyUnit(u, obj);
        }
    }
}