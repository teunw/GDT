using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.Misc;
using Assets._Scripts.Player;
using Assets._Scripts.Units;
using Assets._Scripts.Level.Algorithm;
using UnityEngine;

namespace Assets._Scripts.Level
{
    [RequireComponent(typeof(Tile), typeof(ColorLerpComponent))]
    public class TileSelectableComponent : SelectableComponent
    {
        private Tile tileComponent;

        public void Start()
        {
            tileComponent = GetComponent<Tile>();
        }

        public override bool Select()
        {
//            if (GameManager.getInstance().GetCurrentPlayer.SelectedGameObject != null)
//            {
//                GameObject gmObject = GameManager.getInstance().GetCurrentPlayer.SelectedGameObject;
//                Unit u = gmObject.GetComponent<CubeUnit>() ?? gmObject.GetComponent<SphereUnit>() ?? (Unit) gmObject.GetComponent<ConverterUnit>();
//
//                Algorithm.Algorithm alg = new Algorithm.Algorithm(GameManager.getInstance().Tiles);
//                List<Tile> path = alg.GetPath(tileComponent, u.Tile);
//                return u.PathRange >= path.Count;
//            }
            if (!tileComponent.IsWalkable) return false;
            PlayerComponent currentPlayer = GameManager.getInstance().GetCurrentPlayer;
            PlayerComponent tileOwner = GameManager.getInstance().GetPlayer(tileComponent.PlayerComponent);
            gameObject.GetComponent<ColorLerpComponent>().SelectColor = currentPlayer.unitColor;

            return currentPlayer.SelectedUnit != null || tileOwner.Equals(currentPlayer);
        }
    }
}
