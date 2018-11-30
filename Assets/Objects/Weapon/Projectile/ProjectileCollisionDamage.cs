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
	public class ProjectileCollisionDamage : MonoBehaviour
	{
        Projectile projectile;

        [SerializeField]
        protected float damage = 20f;
        public float Damage
        {
            get
            {
                return damage;
            }
            set
            {
                if (value < 0f) value = 0f;

                damage = value;
            }
        }

        protected virtual void Awake()
        {
            projectile = Dependancy.Get<Projectile>(gameObject, Dependancy.Scope.RecursiveToParents);

            projectile.CollisionEnterEvent += Collision;
        }

        void Collision(Collision collision)
        {
            var target = collision.gameObject.GetComponent<Entity>();

            if (target == null) return;

            projectile.Owner.DoDamage(target, damage);
        }
    }
}