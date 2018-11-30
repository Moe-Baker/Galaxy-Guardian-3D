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
	public class Projectile : MonoBehaviour
	{
        public Entity Owner { get; protected set; }

        public virtual void Init(Entity owner)
        {
            this.Owner = owner;
        }

        public delegate void CollisionDelegate(Collision collision);

        public event CollisionDelegate CollisionEnterEvent;
        public virtual void OnCollisionEnter(Collision collision)
        {
            if (CollisionEnterEvent != null) CollisionEnterEvent(collision);
        }
	}
}