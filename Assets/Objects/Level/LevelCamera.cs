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
    [RequireComponent(typeof(ShakeController))]
    [DefaultExecutionOrder(Level.ExecutionOrder + 1)]
    public class LevelCamera : MonoBehaviour
    {
        public ShakeController Shake { get; protected set; }

        void Awake()
        {
            Shake = GetComponent<ShakeController>();
        }
    }
}