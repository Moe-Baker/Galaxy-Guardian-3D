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
    [RequireComponent(typeof(Image))]
	public class FillProgressModule : ProgressBar.Module
	{
        Image image;

        protected override void GetDependancies()
        {
            base.GetDependancies();

            image = GetComponent<Image>();
            image.type = Image.Type.Filled;
        }

        protected override void UpdateState(float oldValue, float newValue)
        {
            base.UpdateState(oldValue, newValue);

            image.fillAmount = newValue;
        }
    }
}