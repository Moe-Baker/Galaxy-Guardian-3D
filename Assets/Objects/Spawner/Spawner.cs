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
        protected RangeData range = new RangeData(25f, 30f);
        public RangeData Range { get { return range; } }
        [Serializable]
        public struct RangeData
        {
            [SerializeField]
            float minimum;
            public float Minimum { get { return minimum; } }

            [SerializeField]
            float maximum;
            public float Maximum { get { return maximum; } }

            public float Random
            {
                get
                {
                    return UnityEngine.Random.Range(minimum, maximum);
                }
            }

            public RangeData(float minimum, float maximum)
            {
                this.minimum = minimum;
                this.maximum = maximum;
            }
        }

        [SerializeField]
        protected ElementData[] elements;
        public ElementData[] Elements { get { return elements; } }
        [Serializable]
        public class ElementData
        {
            [SerializeField]
            protected GameObject prefab;
            public GameObject Prefab { get { return prefab; } }

            [SerializeField]
            protected int minimumWave;
            public int MinimumWave { get { return minimumWave; } }

            [SerializeField]
            [Range(1, 100)]
            protected int probability;
            public int Probability { get { return probability; } }
        }
        public ElementData GetElement()
        {
            return elements.First();
        }

        [SerializeField]
        protected int waveNumber = 0;
        public int WaveNumber { get { return waveNumber; } }

        public Level Level { get { return Level.Instance; } }
        public GameMenu Menu { get { return Level.Menu; } }
        public PopupLabel PopupLabel { get { return Menu.PopupLabel; } }

        public void Begin()
        {
            StartCoroutine(Procedure());
        }

        public float waveDelay = 5f;
        protected virtual IEnumerator Procedure()
        {
            yield return new WaitForSeconds(waveDelay);

            while (true)
            {
                waveNumber++;

                yield return WaveProcedure();

                yield return new WaitForSeconds(waveDelay);
            }
        }

        protected virtual IEnumerator WaveProcedure()
        {
            PopupLabel.Show("WAVE " + waveNumber);

            var spawnCount = 10 * waveNumber / 2;

            var deathCount = 0;

            Entity.DeathDelegate deathAction = (Entity damager) =>
            {
                deathCount++;
            };

            for (int i = 0; i < spawnCount; i++)
                yield return SpawnProcedure(deathAction);

            while(true)
            {
                if (deathCount >= spawnCount)
                    break;

                yield return new WaitForEndOfFrame();
            }
        }

        public float spawnDelay = 1.5f;
        protected virtual IEnumerator SpawnProcedure(Entity.DeathDelegate deathAction)
        {
            var element = GetElement();

            var rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            var position = Level.Instance.Planet.transform.position + rotation * Vector3.back * range.Random;

            var instance = Instantiate(element.Prefab, position, rotation);

            var entity = instance.GetComponent<Entity>();
            entity.OnDeath += deathAction;

            yield return new WaitForSeconds(spawnDelay / waveNumber);
        }

        public void Stop()
        {
            StopAllCoroutines();

            foreach (var enemy in FindObjectsOfType<EnemyAIController>())
                enemy.Entity.Suicide();

            waveNumber = 0;
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, range.Minimum);

            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, range.Maximum);
        }
#endif
    }
}