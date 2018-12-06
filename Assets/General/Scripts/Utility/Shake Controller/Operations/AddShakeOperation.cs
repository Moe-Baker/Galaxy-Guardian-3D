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
	public class AddShakeOperation : Operation.Behaviour
	{
        public ShakeController controller;

        public float value = 0.5f;

        public override void Execute()
        {
            controller.Value += value;
        }
    }
}