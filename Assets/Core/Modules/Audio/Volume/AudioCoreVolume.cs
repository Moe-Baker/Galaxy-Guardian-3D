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
    public abstract partial class AudioCoreVolumeBase : Core.Module
    {
        public AudioCore Audio { get { return Core.Audio; } }
        public AudioMixer Mixer { get { return Audio.Mixer; } }

        public AudioMixerGroupController[] Controllers { get; protected set; }
        public AudioMixerGroupController FindController(AudioMixerGroup group)
        {
            for (int i = 0; i < Controllers.Length; i++)
            {
                if (Controllers[i].Target == group)
                    return Controllers[i];
            }

            return null;
        }

        public override void Configure()
        {
            base.Configure();

            Core.SceneAccessor.StartCoroutine(ConfigureProcedure());
        }

        public virtual IEnumerator ConfigureProcedure()
        {
            yield return null;

            //audio mixer doesn't get configured internally untill this point
            ConfigureControls();
        }

        protected virtual void ConfigureControls()
        {
            var groups = Mixer.FindMatchingGroups("");

            Controllers = new AudioMixerGroupController[groups.Length];

            for (int i = 0; i < groups.Length; i++)
            {
                Controllers[i] = new AudioMixerGroupController(groups[i]);
                Controllers[i].OnVolumeChanged += OnControllerVolumeChanged;
            }

            Audio.SaveData();
        }

        private void OnControllerVolumeChanged()
        {
            Audio.SaveData();
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

    [CreateAssetMenu(menuName = MenuPath + "Volume")]
	public partial class AudioCoreVolume : AudioCoreVolumeBase
    {
		
	}

    [Serializable]
    public class AudioMixerGroupController
    {
        public AudioMixerGroup Target { get; protected set; }

        public AudioMixer Mixer { get { return Target.audioMixer; } }

        public string Parameter { get; protected set; }

        float volume;
        public virtual float Volume
        {
            get
            {
                if (Mixer.GetFloat(Parameter, out volume))
                {
                    volume = AudioCoreVolume.DecibelToLinear(volume);

                    return volume;
                }
                else
                {
                    throw new InvalidOperationException("A parameter named " + Parameter + " is needed to manipulate the " + Target.name + " Mixer Group's volume");
                }
            }
            set
            {
                value = Mathf.Clamp01(value);

                if (Mixer.SetFloat(Parameter, AudioCoreVolume.LinearToDecibel(value)))
                {
                    volume = value;

                    Core.Asset.Audio.Data.SetVolume(Target.name, volume);

                    if (OnVolumeChanged != null) OnVolumeChanged();
                }
                else
                {
                    throw new InvalidOperationException("A parameter named " + Parameter + " is needed to manipulate the " + Target.name + " Mixer Group's volume");
                }
            }
        }
        public event Action OnVolumeChanged;

        public AudioMixerGroupController(AudioMixerGroup target)
        {
            this.Target = target;

            Parameter = Target.name + " Volume";

            Volume = Core.Asset.Audio.Data.GetVolume(Target.name, Volume);
        }
    }
}