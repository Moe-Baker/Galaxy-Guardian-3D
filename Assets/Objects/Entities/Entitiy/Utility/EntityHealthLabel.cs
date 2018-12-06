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
	public class EntityHealthLabel : MonoBehaviour
	{
        Text label;

        Entity entity;

        public string seperator = "/";

        void Start()
        {
            label = GetComponent<Text>();

            entity = Dependancy.Get<Entity>(gameObject, Dependancy.Scope.RecursiveToParents);

            entity.OnHealthChange += OnChange;

            UpdateState();
        }

        void OnChange(float newValue)
        {
            UpdateState();
        }

        void UpdateState()
        {
            label.text = entity.Health.ToString("N0") + seperator + entity.MaxHealth.ToString("N0");
        }
    }
}