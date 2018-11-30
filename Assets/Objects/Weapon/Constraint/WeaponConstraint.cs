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
	public abstract class WeaponConstraint : MonoBehaviour
	{
        protected Weapon weapon;

        public abstract bool Active { get; }

        protected virtual void Awake()
        {
            weapon = Dependancy.Get<Weapon>(gameObject, Dependancy.Scope.RecursiveToParents);
        }
	}
}