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
	public class FPSDisplay : MonoBehaviour
	{
        GUIStyle style;

        protected virtual void Start()
        {
            style = new GUIStyle() { fontSize = 80, fontStyle = FontStyle.Bold };
        }

        int value;
        protected virtual void Update()
        {
            value = (int)(1f / Time.unscaledDeltaTime);
        }

		protected virtual void OnGUI()
        {
            GUI.Label(new Rect(20f, 20f, 200f, 200f), value.ToString(), style);
        }
	}
}