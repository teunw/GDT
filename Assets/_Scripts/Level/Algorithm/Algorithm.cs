using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.Misc;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets._Scripts.Level.Algorithm
{
    public class Algorithm
    {
        public const bool Debug = false;

        private List<Tile> closedTiles;

        public List<Tile> GetPath(Tile from, Tile to, List<Tile> avoid = null)
        {
            if (avoid == null)
                avoid = GameManager.getInstance().Tiles.FindAll(o => !o.IsWalkable);

            List<Tile> path = new List<Tile>();
            closedTiles = new List<Tile>(avoid);

            Tile currentTile = from;
            path.Add(from);
            while (true)
            {
                List<Tile> ts = GetSurroundingTiles(currentTile);
                ts.RemoveAll(o => closedTiles.Contains(o));
                closedTiles.AddRange(ts);
                Tile closestTile = null;
                float distance = float.MaxValue;
                foreach (Tile tsi in ts)
                {
                    float newTileDistance = Vector2.Distance(tsi.GetGridPosition, to.GetGridPosition);
                    if (newTileDistance < distance)
                    {
                        distance = newTileDistance;
                        closestTile = tsi;
                    }
                    if (path.Contains(to))
                        return path;
                }
                path.Add(closestTile);
                currentTile = closestTile;
            }
        }

        public List<Tile> GetSurroundingTiles(Tile tile)
        {
            if (tile == null) throw new ArgumentNullException("Tile cant be null");
            List<Tile> tiles = new List<Tile>();
            Vector2[] positions = {
                new Vector2(tile.GetGridPosition.x + 1, tile.GetGridPosition.y),
                new Vector2(tile.GetGridPosition.x - 1, tile.GetGridPosition.y),
                new Vector2(tile.GetGridPosition.x, tile.GetGridPosition.y + 1),
                new Vector2(tile.GetGridPosition.x, tile.GetGridPosition.y - 1)
            };
            tiles.AddRange(GameManager.getInstance().Tiles.FindAll(o => positions.Contains(o.GetGridPosition)));
            return tiles;
        }
    }
}
