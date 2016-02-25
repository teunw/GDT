using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts
{
    public class PlayerComponent : MonoBehaviour
    {
        private Camera _camera;
        private Transform _transform;
        private PlayerControls _playerControls;

        public bool IsTurn;
        public GameObject cubeUnit;

        public PlayerResourcesManager ResourcesManager { get; private set; }

        void Start()
        {
            ResourcesManager = new PlayerResourcesManager();
            _playerControls = new PlayerControls();

            _camera = GetComponent<Camera>();

            _playerControls.pitch = _camera.transform.eulerAngles.x;
            _playerControls.yaw = _camera.transform.eulerAngles.y;

            _transform = GetComponent<Transform>();

            GameManager.getInstance().Players.Add(this);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            ResourcesManager.Update(IsTurn);

            _transform.Translate(Vector3.right*_playerControls.getHorizontal());
            _transform.Translate(Vector3.forward*_playerControls.getVertical());

            _playerControls.UpdateCamera(_transform);
        }

        public void BuyUnit()
        {
            CubeScript cs = cubeUnit.GetComponent<CubeScript>();
            if (ResourcesManager.BuyUnit(cs))
            {
                Instantiate(cubeUnit);
            }
            else
            {
                Debug.Log("Nope");
            }
        }
    }
}