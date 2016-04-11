using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Assets._Scripts.Level.Algorithm;
using Assets._Scripts.Misc;
using Assets._Scripts.Player;
using Assets._Scripts.Units;
using UnityEngine;

namespace Assets._Scripts.Level
{
    [RequireComponent(typeof(Transform), typeof (ColorLerpComponent))]
    public class Tile : MonoBehaviour
    {
        [HideInInspector]
        public Color GizmoColor = Color.red;

        public float ExtraHeight;

        [HideInInspector]
        public int PlayerComponent;

        private Vector3 _pos;
        [HideInInspector]
        public Transform Transform;

        void Start()
        {
            Transform = GetComponent<Transform>();
            _pos = Transform.position;
            _pos.y += ExtraHeight;
            GameManager.getInstance().Tiles.Add(this);
        }


        void OnMouseDown()
        {
            // Check if the position already has a unit
            if (!GameManager.getInstance().IsTileWalkable(_pos)) return;

            // Check if unit was selected
            Unit selectedUnit = GameManager.getInstance().GetCurrentPlayer.SelectedUnit;
            if (selectedUnit != null)
            {
                selectedUnit.MoveTo(this);
                GameManager.getInstance().GetCurrentPlayer.SelectedUnit = null;
                return;
            }

            // Check if tile belongs to player
            if (GameManager.getInstance().GetCurrentPlayer != GetCurrentPlayerComponent) return;

            // Check if player can buy unit
            if (!GameManager.getInstance().GetCurrentPlayer.BuyUnit(_pos, this)) return;

            // Unit is spawned, moving to next player
            GameManager.getInstance().GoToNextPlayer();
        }

        void OnDrawGizmos()
        {
            if (!EditorWalkable)
            {
                if (Transform == null) Transform = GetComponent<Transform>();
                Gizmos.color = GizmoColor;
                float m = 1.1f;
                Gizmos.DrawCube(Transform.position, new Vector3(1*m,1 * m, 1 * m));
            }
        }

        public PlayerComponent GetCurrentPlayerComponent
        {
            get { return GameManager.getInstance().GetPlayer(PlayerComponent); }
        }

        public Vector2 GetGridPosition
        {
            get { return new Vector2(Transform.position.x, Transform.position.z); }
        }

        public bool IsWalkable
        {
            get
            {
                if (!EditorWalkable) return false;
                return GameManager.getInstance().Units.Find(o => o.Tile.Equals(this)) == null;
            }
        }

        public bool EditorWalkable;
    }
}