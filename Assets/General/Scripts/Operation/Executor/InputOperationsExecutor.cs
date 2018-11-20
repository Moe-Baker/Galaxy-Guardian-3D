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
	public abstract class InputOperationsExecutor<TComponent> : MonoBehaviour
    {
        public GameObject target;
        public Operation.GameObjectExecutionScope scope = Operation.GameObjectExecutionScope.EntireGameObject;

        protected virtual void Execute()
        {
            Operation.ExecuteIn(target, scope);
        }

        protected virtual void Reset()
        {
            target = gameObject;
        }

        protected virtual void Start()
        {
            Init(GetComponent<TComponent>());
        }

        protected virtual void Init(TComponent component)
        {

        }
    }
}