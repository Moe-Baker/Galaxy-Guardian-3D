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
	public class LevelPause : MonoBehaviour
	{
		[SerializeField]
        protected LevelPauseState state;
        public LevelPauseState State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;

                if (OnChanged != null) OnChanged(state);
            }
        }

        public event Action<LevelPauseState> OnChanged;

        public Menu Menu { get { return Level.Instance.Menu.Pause; } }
    }

    public enum LevelPauseState
    {
        None, Soft, Full
    }
}