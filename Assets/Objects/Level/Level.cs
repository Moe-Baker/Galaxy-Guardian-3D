using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game
{
    [DefaultExecutionOrder(ExecutionOrder)]
	public class Level : MonoBehaviour
	{
        public const int ExecutionOrder = -100;

        public static Level Instance { get; protected set; }

        public Planet Planet { get; protected set; }

        public Player Player { get; protected set; }

        public GameMenu Menu { get; protected set; }

        public Spawner Spawner { get; protected set; }

        protected virtual void Awake()
        {
            Instance = this;

            Planet = FindObjectOfType<Planet>();
            Planet.Entity.OnDeath += OnPlanetDied;

            Player = FindObjectOfType<Player>();
            Player.Entity.OnDeath += OnPlayerDied;

            Menu = FindObjectOfType<GameMenu>();

            Spawner = FindObjectOfType<Spawner>();
        }

        public virtual void Begin()
        {
            Spawner.Begin();

            Player.weapon = true;
        }

        void OnPlayerDied(Entity damager)
        {
            End();
        }

        void OnPlanetDied(Entity damager)
        {
            End();
        }

        void End()
        {
            Spawner.Stop();
        }
    }
}