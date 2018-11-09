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
    public abstract partial class AudioCoreVolumeBase : AudioCore.Module
    {
        public AudioMixer Mixer { get { return Audio.Mixer; } }

        public Controller[] Controls { get; protected set; }
        public Controller FindController(AudioMixerGroup group)
        {
            for (int i = 0; i < Controls.Length; i++)
            {
                if (Controls[i].Target == group)
                    return Controls[i];
            }

            return null;
        }
        [Serializable]
        public class Controller
        {
            public AudioMixerGroup Target { get; protected set; }

            public AudioMixer Mixer { get { return Target.audioMixer; } }

            public string Parameter { get; protected set; }

            float decibalVolume;
            public virtual float DecibalVolume
            {
                get
                {
                    if (Mixer.GetFloat(Parameter, out decibalVolume))
                    {
                        return decibalVolume;
                    }
                    else
                    {
                        throw new InvalidOperationException("A parameter named " + Parameter + " is needed to manipulate the " + Target.name + " Mixer Group's volume");
                    }
                }
                set
                {
                    if (Mixer.SetFloat(Parameter, value))
                    {
                        decibalVolume = value;
                    }
                    else
                    {
                        throw new InvalidOperationException("A parameter named " + Parameter + " is needed to manipulate the " + Target.name + " Mixer Group's volume");
                    }
                }
            }

            public virtual float LinearVolume
            {
                get
                {
                    return AudioCoreVolume.DecibelToLinear(decibalVolume);
                }
                set
                {
                    value = Mathf.Clamp01(value);

                    DecibalVolume = AudioCoreVolume.LinearToDecibel(value);
                }
            }

            public Controller(AudioMixerGroup target)
            {
                this.Target = target;

                Parameter = Target.name + " Volume";

                decibalVolume = DecibalVolume;
            }
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

            Controls = new Controller[groups.Length];

            for (int i = 0; i < groups.Length; i++)
            {
                Controls[i] = new Controller(groups[i]);
            }
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
}