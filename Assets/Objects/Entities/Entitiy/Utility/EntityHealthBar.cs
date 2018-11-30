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
    [RequireComponent(typeof(ProgressBar))]
	public class EntityHealthBar : MonoBehaviour
	{
        [SerializeField]
        protected Entity entity;
        public Entity Entity { get { return entity; } }

        ProgressBar bar;

        void Awake()
        {
            bar = GetComponent<ProgressBar>();

            entity.OnHealthChange += OnChange;

            bar.Value = entity.Health / entity.MaxHealth;
        }

        void OnChange(float newValue)
        {
            bar.Value = entity.Health / entity.MaxHealth;
        }
    }
}