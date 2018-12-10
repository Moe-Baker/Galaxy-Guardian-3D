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
    [RequireComponent(typeof(Entity))]
    [DefaultExecutionOrder(Level.ExecutionOrder + 1)]
    public class Player : MonoBehaviour
	{
		public Entity Entity { get; protected set; }

        public PlayerInput Input { get; protected set; }
        public PlayerNavigator Navigator { get; protected set; }

        public Weapon weapon;

        public bool hasControl = false;

        protected virtual void Awake()
        {
            Entity = GetComponent<Entity>();

            Input = Dependancy.Get<PlayerInput>(gameObject);

            Navigator = Dependancy.Get<PlayerNavigator>(gameObject);

            weapon.Init(Entity);
        }

        protected virtual void Update()
        {
            if(hasControl)
            {
                Input.Process();

                Navigator.Process();

                ProcessWeapons();
            }
        }

        protected virtual void ProcessWeapons()
        {
            var angle = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, PlayerNavigator.VectorToAngle(Input.Direction)));

            weapon.Process(Input.Shoot && (!Application.isMobilePlatform || angle < 10f));
        }
	}
}