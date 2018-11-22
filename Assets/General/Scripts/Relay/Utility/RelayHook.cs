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
	public abstract class RelayHook : MonoBehaviour
	{
        protected virtual void Start()
        {
            var relay = GetComponent<Relay>();

            if (relay == null)
                throw Dependancy.FormatException(nameof(Relay), GetType().Name);

            relay.OnAction += Action;
        }

        protected virtual void Action()
        {

        }
	}
}