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
	public class EnemyExecuteOperationOnDeath : MonoBehaviour
	{
        public GameObject target;
        public Operation.GameObjectExecutionScope scope = Operation.GameObjectExecutionScope.RecursiveToChildern;

        protected virtual void Reset()
        {
            target = gameObject;
        }

        void Start()
        {
            var entity = Dependancy.Get<Entity>(gameObject, Dependancy.Scope.RecursiveToParents);

            entity.OnDeath += OnDeath;
        }

        void OnDeath(Entity damager)
        {
            Operation.ExecuteIn(target, scope);
        }
    }
}