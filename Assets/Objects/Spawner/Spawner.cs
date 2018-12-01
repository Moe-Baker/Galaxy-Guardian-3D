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
	public class Spawner : MonoBehaviour
	{
        [SerializeField]
        protected float radius = 20f;
        public float Radius { get { return radius; } }

        [SerializeField]
        protected float range = 4f;
        public float Range { get { return range; } }

        public float RandomDistance
        {
            get
            {
                return radius + Random.Range(-range, range);
            }
        }

        public float RandomAngle
        {
            get
            {
                return Random.Range(0f, 360f);
            }
        }

        public virtual Vector3 GetPosition(Quaternion rotation, float distance)
        {
            return transform.position + (rotation * Vector3.back) * RandomDistance;
        }
        public virtual Vector3 GetPosition(Quaternion rotation)
        {
            return GetPosition(rotation, RandomDistance);
        }

        public virtual Vector3 GetRandomPosition()
        {
            return GetPosition(Quaternion.Euler(0f, RandomAngle, 0f));
        }

        [SerializeField]
        protected GameObject prefab;
        public GameObject Prefab { get { return prefab; } }

        public virtual void Spawn()
        {
            var rotation = Quaternion.Euler(0f, RandomAngle, 0f);

            var position = GetPosition(rotation);

            var instance = Instantiate(prefab, position, rotation);
            instance.GetComponent<CanvasRenderer>();
        }

        public virtual void Begin()
        {
            coroutine = StartCoroutine(Procedure());
        }

        Coroutine coroutine;
        public bool IsActive { get { return coroutine != null; } }
        public float spawnTime = 2f;
        IEnumerator Procedure()
        {
            while(true)
            {
                Spawn();

                yield return new WaitForSeconds(spawnTime);
            }
        }

        public virtual void Stop()
        {
            if(coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }

            var enemies = FindObjectsOfType<EnemyAIController>();

            foreach (var enemy in enemies)
                enemy.Entity.DoDamage(enemy.Entity, enemy.Entity.Health);
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {

            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, radius);

            Handles.color = Color.yellow;
            Handles.DrawWireDisc(transform.position, Vector3.up, radius + range);
            Handles.DrawWireDisc(transform.position, Vector3.up, radius - range);
        }
#endif
    }
}