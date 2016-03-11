using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Assets._Scripts.Player;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets._Scripts.Net
{
    public class OnlineGamemanager : NetworkBehaviour
    {

        private static OnlineGamemanager instance;

        public static OnlineGamemanager getInstance()
        {
            return instance;
        }

        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
            {

                //if not, set instance to this
                instance = this;
            }

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

        }

        public static List<PlayerComponent> Players = new List<PlayerComponent>();

        public GameObject PlayerPrefab;

        public static void AddPlayer(GameObject player, int num, Color c, string name)
        {
            GameObject playerClone = Instantiate(player);

            PlayerComponent playerComponent = playerClone.GetComponent<PlayerComponent>();
            if (playerComponent) playerComponent = playerClone.AddComponent<PlayerComponent>();
            playerComponent.Color = c;
            playerComponent.PlayerName = name;

            Players.Add(playerComponent);
        }

    }
}
