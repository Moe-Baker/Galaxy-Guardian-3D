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
    [RequireComponent(typeof(Dropdown))]
	public class VideoQualityDropdown : MonoBehaviour
	{
		void Start()
        {
            var dropdown = GetComponent<Dropdown>();

            dropdown.ClearOptions();

            foreach (var name in QualitySettings.names)
                dropdown.options.Add(new Dropdown.OptionData(name));

            dropdown.RefreshShownValue();

            dropdown.value = Core.Asset.Video.Quality;

            dropdown.onValueChanged.AddListener(OnValueChanged);
        }

        void OnValueChanged(int value)
        {
            Core.Asset.Video.Quality = value;
        }
	}
}