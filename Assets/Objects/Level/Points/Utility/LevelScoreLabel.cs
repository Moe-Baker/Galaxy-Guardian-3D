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
    [RequireComponent(typeof(Text))]
	public class LevelScoreLabel : MonoBehaviour
	{
        public LevelPoints Points { get { return Level.Instance.Points; } }

        Text label;

        public string preffix = "Points: ";
        public string suffix;

        void Awake()
        {
            label = GetComponent<Text>();

            Points.OnChanged += OnPointsChanged;

            UpdateState();
        }

        public void UpdateState()
        {
            label.text = preffix + Points.Value.ToString() + suffix;
        }

        void OnPointsChanged(int newPoints)
        {
            UpdateState();
        }
    }
}