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
        protected float _value;
        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                var oldValue = this._value;

                this._value = value;

                if (OnChange != null) OnChange(oldValue, this._value);

                if (!Application.isPlaying)
                    InvokeEditorStateUpdate(oldValue, this._value);
            }
        }

        public delegate void ChangeDelegate(float oldValue, float newValue);
        public event ChangeDelegate OnChange;

        public interface IEditorStateUpdate
        {
            void EDITOR_UpdateState(float oldValue, float newValue);
        }
        protected virtual void InvokeEditorStateUpdate(float oldValue, float newValue)
        {
            foreach (var item in Dependancy.GetAll<IEditorStateUpdate>(gameObject))
                item.EDITOR_UpdateState(oldValue, newValue);
        }

        public class Module : MonoBehaviour, Initializer.Interface, IEditorStateUpdate
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

            public virtual void EDITOR_UpdateState(float oldValue, float newValue)
            {
                GetDependancies();

                UpdateState(oldValue, newValue);
            }
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(ProgressBar))]
        public class Inspector : Editor
        {
            new ProgressBar target;

            void OnEnable()
            {
                target = base.target as ProgressBar;
            }

            public override void OnInspectorGUI()
            {
                var value = target.Value;

                value = EditorGUILayout.Slider("Value", value, 0f, 1f);

                if (target.Value != value)
                    target.Value = value;
            }
        }
#endif
    }
}