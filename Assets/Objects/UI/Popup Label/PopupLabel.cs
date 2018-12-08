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
    public class PopupLabel : MonoBehaviour, Initializer.Interface
    {
        Text label;
        public Text Label { get { return label; } }

        public void Init()
        {
            label = GetComponent<Text>();
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        public void Show(string text)
        {
            label.text = text;

            Show();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
	}
}