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

using System.Runtime.Serialization;

namespace Game
{
    public abstract class AudioCoreBase : Core.Module
    {
        new public const string MenuPath = Core.Module.MenuPath + "Audio/";

        [SerializeField]
        protected AudioMixer mixer;
        public AudioMixer Mixer { get { return mixer; } }

        public List<AudioMixerGroupController> MixerGroupControllers { get; protected set; }
        protected virtual void ConfigureMixerGroupControllers()
        {
            MixerGroupControllers = new List<AudioMixerGroupController>();

            var groups = mixer.FindMatchingGroups("");

            foreach (var group in groups)
                MixerGroupControllers.Add(new AudioMixerGroupController(group));
        }

        public override void Configure()
        {
            base.Configure();

            Core.Asset.SceneAccessor.Coroutine.YieldFrame(ConfigureMixerGroupControllers);
        }

        public static float LinearToDecibel(float linear)
        {
            if (linear == 0)
                return -144.0f;
            else
                return 20.0f * Mathf.Log10(linear);
        }
        public static float DecibelToLinear(float dB)
        {
            return Mathf.Pow(10.0f, dB / 20.0f);
        }
    }

    [CreateAssetMenu(menuName = MenuPath + "Asset")]
	public partial class AudioCore : AudioCoreBase
    {
        
    }

    [Serializable]
    public class AudioMixerGroupController
    {
        public AudioMixerGroup MixerGroup { get; protected set; }

        public AudioMixer Mixer { get { return MixerGroup.audioMixer; } }

        public string Parameter { get { return MixerGroup.name + " Volume"; } }

        public virtual float Volume
        {
            get
            {
                return PlayerPrefs.GetFloat(Parameter);
            }
            set
            {
                value = Mathf.Clamp01(value);

                Mixer.SetFloat(Parameter, AudioCore.LinearToDecibel(value));

                PlayerPrefs.SetFloat(Parameter, value);
            }
        }

        public AudioMixerGroupController(AudioMixerGroup mixerGroup)
        {
            this.MixerGroup = mixerGroup;

            float volume;

            if(Mixer.GetFloat(Parameter, out volume))
            {
                volume = AudioCore.DecibelToLinear(volume);

                if(PlayerPrefs.HasKey(Parameter))
                {
                    Volume = PlayerPrefs.GetFloat(Parameter, volume);
                }
                else
                {
                    PlayerPrefs.SetFloat(Parameter, volume);
                }
            }
            else
            {
                throw new InvalidOperationException("A parameter named " + Parameter + " is needed to manipulate the " + MixerGroup.name + " Mixer Group's volume");
            }
        }
    }
}