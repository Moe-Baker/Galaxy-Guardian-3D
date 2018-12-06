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
	public class PauseRelay : Relay
	{
        protected override void Start()
        {
            base.Start();

            Level.Instance.Pause.OnChanged += OnPauseChange;
        }

        void OnPauseChange(LevelPauseState state)
        {
            InvokeEvent();
        }
    }
}