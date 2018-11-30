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
    public class PlayerNavigator : MonoBehaviour
    {
        public Player Player { get { return Level.Instance.Player; } }
        public PlayerInput Input { get { return Player.Input; } }

        public Planet Planet { get { return Level.Instance.Planet; } }

        [SerializeField]
        protected float distanceFromSurface = 1f;
        public float DistanceFromSurface
        {
            get
            {
                return distanceFromSurface;
            }
            set
            {
                if (value < 0f)
                    value = 0f;

                distanceFromSurface = value;
            }
        }

        [SerializeField]
        protected float rotationSpeed = 360f;
        public float RotationSpeed { get { return rotationSpeed; } }

        protected virtual void Update()
        {
            SetDirection(Input.Direction);
        }

        protected virtual void SetDirection(Vector2 vector)
        {
            var angles = Player.transform.eulerAngles;
            angles.y = Mathf.MoveTowardsAngle(angles.y, VectorToAngle(vector), rotationSpeed * Time.deltaTime);
            Player.transform.eulerAngles = angles;

            Player.transform.position = Planet.transform.position + (Player.transform.rotation * Vector3.forward) * (Planet.Radius + distanceFromSurface);
        }

        public static float VectorToAngle(Vector2 vector)
        {
            return Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
        }
    }
}