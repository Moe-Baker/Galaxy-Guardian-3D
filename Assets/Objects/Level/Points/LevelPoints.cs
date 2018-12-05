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
	public class LevelPoints : MonoBehaviour
	{
		[SerializeField]
        protected int value;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;

                if (OnChanged != null) OnChanged(this.value);
            }
        }

        public event Action<int> OnChanged;

        public virtual void Add(int value)
        {
            this.Value += value;
        }

        public virtual void Remove(int value)
        {
            this.Value -= value;
        }
    }
}