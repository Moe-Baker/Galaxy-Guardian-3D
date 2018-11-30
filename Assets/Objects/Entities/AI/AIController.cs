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
	public abstract class AIController : MonoBehaviour
	{
        public Planet Planet { get { return Level.Instance.Planet; } }

        public Player Player { get { return Level.Instance.Player; } }

        public AI AI { get; protected set; }

        public Entity Entity { get { return AI.Entity; } }

        public virtual void Init(AI AI)
        {
            this.AI = AI;
        }
    }
}