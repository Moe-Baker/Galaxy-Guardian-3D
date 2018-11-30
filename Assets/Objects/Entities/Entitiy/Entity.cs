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
	public class Entity : MonoBehaviour
	{
        [SerializeField]
        protected float _health;
        public float Health
        {
            get
            {
                return _health;
            }
            protected set
            {
                _health = value;

                if (OnHealthChange != null)
                    OnHealthChange(Health);
            }
        }
        public delegate void HealthChangeDelegate(float newValue);
        public event HealthChangeDelegate OnHealthChange;

        [SerializeField]
        protected float maxHealth = 100f;
        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }
            set
            {
                if (value < 0)
                    value = 0f;

                maxHealth = value;
            }
        }

        protected virtual void Reset()
        {
            Health = maxHealth;
        }

        public delegate void TakeDamageDelegate(Entity damager, float damage);
        public event TakeDamageDelegate OnTakeDamage;
        public virtual void TakeDamage(Entity damager, float damage)
        {
            if (Health == 0f) return;

            if (damage >= Health)
                Health = 0f;
            else
                Health -= damage;

            if (OnTakeDamage != null)
                OnTakeDamage(damager, damage);

            if (Health == 0f)
                Death(damager);
        }

        public virtual void DoDamage(Entity target, float damage)
        {
            target.TakeDamage(this, damage);
        }

        public delegate void DeathDelegate(Entity damager);
        public event DeathDelegate OnDeath;
        public virtual void Death(Entity damager)
        {
            if (OnDeath != null)
                OnDeath(damager);
        }
    }
}