using UnityEngine;

namespace Assets._Scripts.Level
{
    class MaterialColorLerper : MonoBehaviour
    {

        public const float TIME_LIMIT = .5F;

        public Color OriginalColor;
        public Color CurrentColor;

        private float _timePassed;
        private bool _done;

        private Renderer _colorRenderer;      

        void Start()
        {
            _colorRenderer = GetComponent<Renderer>();
            _done = false;
        }

        void Update()
        {
            if (_done)
            {
                _colorRenderer.material.color = OriginalColor;
                if (_colorRenderer.material.color == OriginalColor)
                    Destroy(this);
                return;
            }
            _timePassed += Time.deltaTime;

            if (_timePassed > TIME_LIMIT)
            {
                _colorRenderer.material.color = OriginalColor;
                _done = true;
            }
            else
            {
                _colorRenderer.material.color = Color.Lerp(CurrentColor, OriginalColor, _timePassed / TIME_LIMIT);
            }
        }

        public void Reset()
        {
            _timePassed = 0;
        }

    }
}