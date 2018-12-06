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
	public class ProgressBar : MonoBehaviour
	{
		[SerializeField]
        [Range(0f, 1f)]
        protected float value;
        public float Value
        {
            get
            {
                return value;
            }
            set
            {
                var oldValue = this.value;

                this.value = value;

                if (OnChange != null) OnChange(oldValue, this.value);

#if UNITY_EDITOR
                if (!Application.isPlaying)
                    SendMessage(Module.EditMessageRecieverTarget, this.value, SendMessageOptions.DontRequireReceiver);
#endif
            }
        }

        public delegate void ChangeDelegate(float oldValue, float newValue);
        public event ChangeDelegate OnChange;

        public class Module : MonoBehaviour, Initializer.Interface
        {
            ProgressBar bar;

            public virtual void Init()
            {
                bar = Dependancy.Get<ProgressBar>(gameObject, Dependancy.Scope.RecursiveToParents);

                bar.OnChange += OnChange;

                GetDependancies();

                UpdateState(bar.Value, bar.Value);
            }

            protected virtual void GetDependancies()
            {

            }

            protected virtual void OnChange(float oldValue, float newValue)
            {
                UpdateState(oldValue, newValue);
            }

            protected virtual void UpdateState(float oldValue, float newValue)
            {

            }

#if UNITY_EDITOR
            public const string EditMessageRecieverTarget = nameof(EDITOR_UpdateState);
            public void EDITOR_UpdateState(float value)
            {
                GetDependancies();

                UpdateState(value, value);
            }
#endif
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(ProgressBar))]
        public class Inspector : Editor
        {

        }
#endif
    }
}