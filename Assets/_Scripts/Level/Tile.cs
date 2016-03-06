using System.Runtime.CompilerServices;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Level
{
    public class Tile : MonoBehaviour
    {

        public float ExtraHeight;

        [HideInInspector]
        public int PlayerComponent;

        private Vector3 _pos;
        private Transform _transform;

        void Start()
        {
            _transform = GetComponent<Transform>();
            _pos = _transform.position;
            _pos.y += ExtraHeight;
        }


        void OnMouseDown()
        {
            // Check if the position already has a unit
            if (GameManager.getInstance().HasUnit(_pos)) return;
            // Check if tile belongs to player
            if (GameManager.getInstance().GetCurrentPlayer != GetCurrentPlayerComponent) return;
            // Check if player can buy unit
            if (!GameManager.getInstance().GetCurrentPlayer.BuyUnit(_pos)) return;

            // Unit is spawned, moving to next player
            GameManager.getInstance().GoToNextPlayer();
        }

        public PlayerComponent GetCurrentPlayerComponent
        {
            get { return GameManager.getInstance().GetPlayer(PlayerComponent); }
        }
    
    }
}
