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

namespace OLD
{
	public class CircleProgress : MonoBehaviour
	{
		[SerializeField]
        protected Image image;
        public Image Image { get { return image; } }

        public virtual float Value
        {
            get
            {
                if (image == null)
                    throw new NullReferenceException("No image set for " + nameof(ProgressBar) + " on " + name);

                return Image.fillAmount;
            }
            set
            {
                if (image == null)
                    throw new NullReferenceException("No image set for " + nameof(ProgressBar) + " on " + name);

                Image.fillAmount = value;

                if (OnValueChange != null) OnValueChange(Value);
            }
        }
        public delegate void ValueChangeDelegate(float newValue);
        public event ValueChangeDelegate OnValueChange;

        protected virtual void Reset()
        {
            var images = GetComponentsInChildren<Image>(true);

            for (int i = 0; i < images.Length; i++)
            {
                if(images[i].name.ToLower().Contains("progress") || images[i].name.ToLower().Contains("value"))
                {
                    image = images[i];
                    break;
                }
            }

            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name.ToLower().Contains("background") || images[i].name.ToLower().Contains("bg"))
                    continue;

                image = images[i];
                break;
            }

            if(image != null)
            {
                Value = 0.5f;
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(CircleProgress))]
        public class Inspector : Editor
        {
            new public CircleProgress target;

            protected virtual void OnEnable()
            {
                target = base.target as CircleProgress;
            }

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                DrawValue();
            }

            protected virtual void DrawValue()
            {
                if (target.image == null)
                {
                    EditorGUILayout.HelpBox("No Image Set", MessageType.Error);
                    return;
                }

                target.Value = EditorGUILayout.Slider(target.Value, 0f, 1f);
            }
        }
#endif
    }
}