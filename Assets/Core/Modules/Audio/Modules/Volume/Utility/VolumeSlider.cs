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

using UnityEngine.Audio;

namespace Game
{
    [RequireComponent(typeof(Slider))]
	public class VolumeSlider : MonoBehaviour
	{
        [SerializeField]
        protected AudioMixerGroup group;
        public AudioMixerGroup Group { get { return group; } }

        AudioCoreVolume.Controller controller;

        protected virtual void Start()
        {
            controller = Core.Asset.Audio.Volume.FindController(group);

            var slider = GetComponent<Slider>();

            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = controller.LinearVolume;

            slider.onValueChanged.AddListener(OnValueChanged);
        }

        protected virtual void OnValueChanged(float newValue)
        {
            controller.LinearVolume = newValue;
        }
    }
}