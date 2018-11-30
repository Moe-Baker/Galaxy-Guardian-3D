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
	public class AI : MonoBehaviour
	{
        public Entity Entity { get; protected set; }

        public AIController Controller { get; protected set; }

        protected virtual void Awake()
        {
            Entity = GetComponent<Entity>();

            Controller = Dependancy.Get<AIController>(gameObject);
            Controller.Init(this);
        }
	}
}