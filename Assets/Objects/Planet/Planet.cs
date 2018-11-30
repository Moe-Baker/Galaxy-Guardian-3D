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
	public class Planet : MonoBehaviour
	{
		public Entity Entity { get; protected set; }

        new public SphereCollider collider { get; protected set; }
        public float Radius { get { return collider.radius; } }

        protected virtual void Awake()
        {
            Entity = GetComponent<Entity>();

            collider = Dependancy.Get<SphereCollider>(gameObject);
        }
	}
}