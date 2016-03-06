using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets._Scripts
{
    public class DayCycle : MonoBehaviour
    {

        public float speed;

        private Transform _transform;

        void Start()
        {
            _transform = GetComponent<Transform>();
        }

        void Update()
        {
            _transform.Rotate(Vector3.left * speed);
        }

    }
}
