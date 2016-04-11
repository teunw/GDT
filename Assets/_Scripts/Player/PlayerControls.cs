using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.Player
{
    class PlayerControls
    {
        public const float CameraSpeed = 2f;
        public const float ScrollSpeed = 500f;
        public float yaw, pitch;

        public PlayerControls(Transform t)
        {
            yaw = t.eulerAngles.y;
            pitch = t.eulerAngles.x;
        }

        public float getHorizontal()
        {
            return Input.GetAxis("Horizontal");
        }

        public float getVertical()
        {
            return Input.GetAxis("Vertical");
        }

        public float getHorizontalMouse()
        {
            return Input.GetAxis("Mouse X");
        }

        public float getVerticalMouse()
        {
            return Input.GetAxis("Mouse Y");
        }

        public bool CameraTurn
        {
            get { return Input.GetMouseButtonDown(1); }
        }

        public void UpdateCamera(Transform transform)
        {
            if (Input.GetMouseButton(2))
            {
                yaw += CameraSpeed*getHorizontalMouse();
                pitch -= CameraSpeed*getVerticalMouse();
                transform.eulerAngles = new Vector3(pitch, yaw, 0f);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                ;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void UpdateZoom(Transform transform)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(Vector3.forward*scroll*Time.deltaTime*ScrollSpeed);
        }
    }
}