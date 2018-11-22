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
    public abstract class Relay : MonoBehaviour
    {
        public event Action OnAction;

        protected virtual void InvokeAction()
        {
            if (!enabled) return;

            if (OnAction != null)
                OnAction();
        }

        protected virtual void Start()
        {
            
        }
    }

	public abstract class Relay<TComponent> : Relay
    {
        protected override void Start()
        {
            base.Start();

            Init(GetComponent<TComponent>());
        }

        protected virtual void Init(TComponent component)
        {

        }
    }
}