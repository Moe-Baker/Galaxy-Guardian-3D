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
	public class EntityDeathByPlayerRelay : Relay
	{
        protected override void Start()
        {
            base.Start();

            var entity = Dependancy.Get<Entity>(gameObject, Dependancy.Scope.RecursiveToParents);

            entity.OnDeath += OnDeath;
        }

        void OnDeath(Entity damager)
        {
            if (damager == Level.Instance.Player.Entity)
                InvokeEvent();
        }
    }
}