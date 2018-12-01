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
	public class EnemyAIController : AIController
	{
        public Transform Target { get { return Planet.transform; } }

        public float rotationSpeed = 40f;

        public float movementSpeed = 4f;

        Quaternion rotation;
        float distance;

        public override void Init(AI AI)
        {
            base.Init(AI);

            distance = Vector3.Distance(transform.position, Target.position);
            rotation = transform.rotation;

            StartCoroutine(Procedure());
        }

        protected virtual IEnumerator Procedure()
        {
            while(true)
            {
                rotation *= Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f);
                distance = Mathf.MoveTowards(distance, 0f, movementSpeed * Time.deltaTime);

                transform.rotation = rotation * Quaternion.Euler(0f, Mathf.Lerp(0f, -45f, (distance - Planet.Radius - 2f) / 4f), 0f);

                transform.position = Target.position +
                    rotation * Vector3.back * distance;

                yield return new WaitForEndOfFrame();
            }
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            var target = collision.gameObject.GetComponent<Entity>();

            if (target == null) return;

            Entity.DoDamage(target, 20f);
            Entity.DoDamage(Entity, Entity.Health);
        }
    }
}