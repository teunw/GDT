using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.Player
{
    class PlayerControls
    {
        public float yaw, pitch;

        public const float CameraSpeed = 2f;

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
            yaw += CameraSpeed*getHorizontalMouse();
            pitch -= CameraSpeed*getVerticalMouse();
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }
}
