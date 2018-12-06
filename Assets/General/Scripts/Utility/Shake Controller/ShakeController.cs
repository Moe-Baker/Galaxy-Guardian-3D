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
	public class ShakeController : MonoBehaviour
	{
        [SerializeField]
        [Range(0f, 1f)]
        protected float value;
        public float Value
        {
            get
            {
                return value;
            }
            set
            {
                value = Mathf.Clamp01(value);

                this.value = value;
            }
        }

        [SerializeField]
        protected float power = 1.5f;
        public float Power { get { return power; } }

        [SerializeField]
        protected float resetSpeed = 2f;
        public float ResetSpeed { get { return resetSpeed; } }

        [SerializeField]
        protected MultiplierData multiplier = new MultiplierData(Vector3.one, Vector3.one);
        public MultiplierData Multiplier { get { return multiplier; } }
        [Serializable]
        public struct MultiplierData
        {
            [SerializeField]
            ElementData position;
            public ElementData Position { get { return position; } }

            [SerializeField]
            ElementData angles;
            public ElementData Angles { get { return angles; } }

            [Serializable]
            public struct ElementData
            {
                [SerializeField]
                float magnitude;
                public float Magnitude { get { return magnitude; } }

                [SerializeField]
                Vector3 vector;
                public Vector3 Vector { get { return vector; } }

                public Vector3 Value { get { return vector * magnitude; } }

                public ElementData(float magnitude, Vector3 vector)
                {
                    this.magnitude = magnitude;
                    this.vector = vector;
                }
                public ElementData(float magnitude) : this(magnitude, Vector3.zero)
                {

                }
                public ElementData(Vector3 vector) : this(1f, vector)
                {

                }
            }

            public MultiplierData(ElementData position, ElementData angles)
            {
                this.position = position;
                this.angles = angles;
            }
            public MultiplierData(Vector3 position, Vector3 angles) : this(new ElementData(position), new ElementData(angles))
            {

            }
            public MultiplierData(Vector3 position) : this(position, Vector3.zero)
            {

            }
        }

        Vector3 initialPosition;
        Vector3 initialAngles;

        public float RandomDirection { get { return Random.Range(-1f, 1f); } }

        void Start()
        {
            initialPosition = transform.localPosition;
            initialAngles = transform.localEulerAngles;
        }

		void LateUpdate()
        {
            transform.localPosition = initialPosition + GetOffset(multiplier.Position.Value);
            transform.localEulerAngles = initialAngles + GetOffset(multiplier.Angles.Value);

            value = Mathf.MoveTowards(value, 0f, resetSpeed * Time.deltaTime);
        }

        Vector3 GetOffset(Vector3 multiplier)
        {
            return new Vector3()
            {
                x = multiplier.x * Mathf.Pow(value, power) * RandomDirection,
                y = multiplier.y * Mathf.Pow(value, power) * RandomDirection,
                z = multiplier.z * Mathf.Pow(value, power) * RandomDirection,
            };
        }
	}
}