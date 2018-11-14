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
	public class MixerGroupVolumeSlider : MonoBehaviour
	{
        [SerializeField]
        protected AudioMixerGroup group;
        public AudioMixerGroup Group { get { return group; } }

        AudioMixerGroupController controller;

        protected virtual void Start()
        {
            Core.Asset.SceneAccessor.Coroutine.YieldFrame(Init);
        }

        protected virtual void Init()
        {
            foreach (var controller in Core.Asset.Audio.MixerGroupControllers)
            {
                if(controller.MixerGroup == group)
                {
                    this.controller = controller;
                    break;
                }
            }

            if (controller == null)
                throw new Exception("No AudioMixerGroupController found for MixerGroup: " + group.name + " Of the Mixer: " + group.audioMixer.name);

            var slider = GetComponent<Slider>();

            slider.minValue = 0f;
            slider.maxValue = 1f;
            slider.value = controller.Volume;

            slider.onValueChanged.AddListener(OnValueChanged);
        }

        protected virtual void OnValueChanged(float newValue)
        {
            controller.Volume = newValue;
        }
    }
}