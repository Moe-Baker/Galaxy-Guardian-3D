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
    [RequireComponent(typeof(RectTransform))]
    public class AnchorProgressModule : ProgressBar.Module
	{
        RectTransform rect;

        protected override void GetDependancies()
        {
            base.GetDependancies();

            rect = GetComponent<RectTransform>();
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = new Vector2(bar.Value, 1f);
        }

        protected override void UpdateState(float oldValue, float newValue)
        {
            base.UpdateState(oldValue, newValue);

            rect.anchorMax = new Vector2(newValue, rect.anchorMax.y);
        }
    }
}