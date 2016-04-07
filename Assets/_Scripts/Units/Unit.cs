using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Assets._Scripts.GameResources;
using Assets._Scripts.Level;
using Assets._Scripts.Level.Algorithm;
using Assets._Scripts.Misc;
using Assets._Scripts.Player;
using UnityEngine;

namespace Assets._Scripts.Units
{
    [RequireComponent(typeof (Transform), typeof (ColorLerpComponent))]
    public abstract class Unit : MonoBehaviour, Buyable, TurnListener
    {
        private bool hasWalkedThisTurn;

        public Transform Transform { get; private set; }
        public PlayerComponent PlayerComponent;
        public ColorLerpComponent ColorLerpComponent { get; private set; }

        public List<Tile> pathToFollow;
        public int currentTile;
        public float distanceLerp;
        public Vector3 from;

        public Tile Tile;

        public virtual void Start()
        {
            Transform = GetComponent<Transform>();
            ColorLerpComponent = GetComponent<ColorLerpComponent>();
            DeleteMovement();
            GameManager.getInstance().Units.Add(this);
            GameManager.getInstance().AddTurnListener(this);
        }

        public abstract string Name { get; }

        public abstract int MovementEnergy { get; }

        public abstract List<BuyRequirement> Requirements { get; }

        public void OnMouseDown()
        {
            if (PlayerComponent != GameManager.getInstance().GetCurrentPlayer) return;

            if (hasWalkedThisTurn)
            {
                GameManager.getInstance().ErrorText.text = "Unit has already walked this turn";
                return;
            }

            ColorLerpComponent.Activate();

            PlayerComponent.SelectedUnit = this;
        }

        public void OnNextTurn(PlayerComponent oldPlayer, PlayerComponent newPlayer)
        {
            hasWalkedThisTurn = false;
        }

        public virtual void Update()
        {
            if (pathToFollow != null)
            {
                distanceLerp += Speed*Time.deltaTime;
                if (distanceLerp > 1f)
                {
                    if (pathToFollow.Count - 1 <= currentTile)
                    {
                        KillNearbyUnits();
                        DeleteMovement();
                        return;
                    }
                    ResetMovement();
                }
                Tile nxt = pathToFollow[currentTile];
                Vector2 nxtPosition = Vector2.Lerp(ConvertGrid(from), ConvertGrid(nxt.Transform.position), distanceLerp);
                Vector3 currentPosition = new Vector3(nxtPosition.x, Transform.position.y, nxtPosition.y);
                Transform.position = currentPosition;
            }
        }

        public virtual void KillNearbyUnits()
        {
            Collider[] colliders = Physics.OverlapBox(Transform.position, Range);
            foreach (Collider c in colliders)
            {
                ColorLerpComponent colorLerpComponent = c.GetComponent<ColorLerpComponent>();
                if (colorLerpComponent != null)
                {
                    colorLerpComponent.Activate(PlayerComponent.unitColor);
                }
                Unit unit = c.GetComponent<CubeUnit>() ?? (Unit) c.GetComponent<SphereUnit>();
                if (unit != null)
                {
                    if (unit.PlayerComponent == this.PlayerComponent) continue;
                    Destroy(unit.gameObject);
                }
            }
        }

        public void MoveTo(Tile tile)
        {
            DeleteMovement();
            Algorithm alg = new Algorithm();
            pathToFollow = alg.GetPath(this.Tile, tile);

            if (pathToFollow.Count > PathRange)
            {
                pathToFollow = null;
                TurnNotAvailable();
                return;
            }

            this.from = Transform.position;
            ResetMovement();
            hasWalkedThisTurn = true;
        }

        private void TurnNotAvailable()
        {
            GameManager.getInstance().ErrorText.text = "Unit doesnt have enough range";
        }

        public void DeleteMovement()
        {
            if (pathToFollow != null && pathToFollow != null && pathToFollow.Count >= 1)
                this.Tile = pathToFollow[pathToFollow.Count - 1];
            pathToFollow = null;
            currentTile = 0;
            distanceLerp = 0.0f;
        }

        public void ResetMovement()
        {
            currentTile += 1;
            distanceLerp = 0.0f;
            this.from = Transform.position;
        }

        public void OnTriggerEnter(Collider other)
        {
            CoinClickable coin = other.GetComponent<CoinClickable>();
            if (coin == null) return;
            coin.OnMouseDown();
        }

        public Vector2 ConvertGrid(Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }

        public virtual float Speed { get { return 5f; } }

        public virtual Vector3 Range { get { return new Vector3(3f, 2f, 3f); } }

        public virtual float PathRange { get { return 6f; } }
    }
}