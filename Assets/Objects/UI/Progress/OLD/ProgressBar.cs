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
	public class ProgressBar : MonoBehaviour
	{
		[SerializeField]
        protected Image image;
        public Image Image { get { return image; } }
        protected virtual void SetUpAnchors()
        {
            if (image == null) return;

            Image.rectTransform.anchorMin = Vector2.zero;
            Image.rectTransform.anchorMax = new Vector2(Value, 1f);
        }

        public virtual float Value
        {
            get
            {
                if (image == null)
                    throw new NullReferenceException("No image set for " + nameof(ProgressBar) + " on " + name);

                return Image.rectTransform.anchorMax.x;
            }
            set
            {
                if (image == null)
                    throw new NullReferenceException("No image set for " + nameof(ProgressBar) + " on " + name);

                Image.rectTransform.anchorMax = new Vector2(value, Image.rectTransform.anchorMax.y);

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
                SetUpAnchors();
                Value = 0.5f;
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(ProgressBar))]
        public class Inspector : Editor
        {
            new public ProgressBar target;

            protected virtual void OnEnable()
            {
                target = base.target as ProgressBar;
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