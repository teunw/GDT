using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Misc
{
    [RequireComponent(typeof(Renderer))]
    public class ColorLerpComponent : MonoBehaviour
    {

        public const float TIME_LIMIT = .5F;
        public static readonly Color DefaultColor = new Color(-1, -1, -1, -1);

        [HideInInspector]
        public Color OriginalColor;
        public Color SelectColor = Color.black;
        [HideInInspector]
        public Color CustomSelectColor = Color.clear;
        public float Speed = 2f;
        public bool StartActivated = false;
        
        private float _timePassed;
        private bool _done;
        private bool _keepActivated;
        private Renderer _colorRenderer;

        void Start()
        {
            _colorRenderer = GetComponent<Renderer>();
            OriginalColor = _colorRenderer.material.color;
            _done = !StartActivated;
        }

        void Update()
        {
            if (_done) return;
            if (CustomSelectColor != Color.clear)
            {
                _colorRenderer.material.color = Color.Lerp(CustomSelectColor, OriginalColor, _timePassed/Speed);
            }
            else
            {
                _colorRenderer.material.color = Color.Lerp(SelectColor, OriginalColor, _timePassed/Speed);
            }
            if (_keepActivated)
            {
                _timePassed = 0;
                return;
            }
            _timePassed += Time.deltaTime;
            if (_timePassed > Speed)
            {
                _done = true;
                CustomSelectColor = Color.clear;
            }
        }

        public void Activate(bool keepActivated = false)
        {
            _keepActivated = keepActivated;
            _done = false;
            _timePassed = 0;
        }

        public void Activate(Color customColor)
        {
            _done = false;
            _timePassed = 0;
            CustomSelectColor = customColor;
        }

        public void Deactivate()
        {
            _keepActivated = false;
        }
    }
}
