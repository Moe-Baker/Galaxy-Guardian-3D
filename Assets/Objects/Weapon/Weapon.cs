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
	public class Weapon : MonoBehaviour
	{
        public Entity Owner { get; protected set; }

        public virtual void Init(Entity owner)
        {
            this.Owner = owner;
        }

        public event Action OnProcess;
        public virtual void Process()
        {
            if (OnProcess != null)
                OnProcess();
        }

        public event Action OnAction;
        public virtual void Action()
        {
            if (OnAction != null)
                OnAction();
        }
	}
}