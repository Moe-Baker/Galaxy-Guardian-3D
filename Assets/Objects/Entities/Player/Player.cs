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
	public class Player : MonoBehaviour
	{
		public Entity Entity { get; protected set; }

        public PlayerInput Input { get; protected set; }
        public PlayerNavigator Navigator { get; protected set; }

        public Weapon weapon;

        protected virtual void Awake()
        {
            Entity = GetComponent<Entity>();

            Input = Dependancy.Get<PlayerInput>(gameObject);

            Navigator = Dependancy.Get<PlayerNavigator>(gameObject);
        }

        protected virtual void Update()
        {
            Input.Process();

            weapon.Process(Input.Shoot);
        }
	}
}